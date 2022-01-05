using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using RentCarProject.DataAccess.Repository.IRepository;
using RentCarProject.Diger;
using RentCarProject.Models;
using RentCarProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace RentCarProject.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        //onaylanmış mailleri kabul edr sepete eklemeyi 
        private readonly UserManager<IdentityUser> _userManager;
        [BindProperty]
        public ReservationCartVM ReservationCartVM{ get; set; }
        public CartController(UserManager<IdentityUser> userManager,
            IUnitOfWork unitOfWork,
            IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _emailSender = emailSender;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ReservationCartVM = new ReservationCartVM()
            {
                OrderHeader = new Models.OrderHeader(),
                ListCart = _unitOfWork.ReservationCart.GetAll(i => i.ApplicationUserId == claim.Value, includeProperties: "Vehicle")
        };
            ReservationCartVM.OrderHeader.OrderTotal = 0;
            ReservationCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(i => i.Id == claim.Value);
            foreach (var item in ReservationCartVM.ListCart)
            {
                ReservationCartVM.OrderHeader.OrderTotal += (item.Count * item.Vehicle.DailyRentalPrice);
            }
            return View(ReservationCartVM);
        }
        [HttpPost]
        [ActionName("Index")]
        public async Task<IActionResult> IndexPost()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var user = _unitOfWork.ApplicationUser.GetFirstOrDefault(i => i.Id == claim.Value);//o an giren müşteriyi bulur
            if (user==null)
            {
                ModelState.AddModelError(string.Empty,"Dogrulama email boş...");
            }
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = user.Id, code = code},
                protocol: Request.Scheme);

            await _emailSender.SendEmailAsync(user.Email, "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
            ModelState.AddModelError(string.Empty,"Email dogrulama kodu gönder...");
            return RedirectToAction("Success");
        }
        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ReservationCartVM = new ReservationCartVM()
            {
                OrderHeader = new Models.OrderHeader(),
                ListCart = _unitOfWork.ReservationCart.GetAll(i => i.ApplicationUserId == claim.Value,
                includeProperties: "Vehicle")

            };
            foreach (var item in ReservationCartVM.ListCart)
            {
                item.Price = item.Vehicle.DailyRentalPrice;
                ReservationCartVM.OrderHeader.OrderTotal += (item.Count * item.Vehicle.DailyRentalPrice);
            }
            return View(ReservationCartVM);
        }
        [HttpPost]
        [ActionName("Summary")]
        [ValidateAntiForgeryToken]
        public IActionResult SummaryPOST(ReservationCartVM model)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ReservationCartVM.ListCart = _unitOfWork.ReservationCart.GetAll(i => i.ApplicationUserId == claim.Value,
                includeProperties: "Vehicle");
            ReservationCartVM.OrderHeader.OrderStatus = SD.Durum_Beklemede;
            ReservationCartVM.OrderHeader.ApplicationUserId = claim.Value;
            ReservationCartVM.OrderHeader.OrderDate = DateTime.Now;
            _unitOfWork.OrderHeader.Add(ReservationCartVM.OrderHeader);
            _unitOfWork.Save();
            foreach (var item in ReservationCartVM.ListCart)
            {
                item.Price = item.Vehicle.DailyRentalPrice;
                OrderDetails orderDetails = new OrderDetails()
                {
                    VehicleId = item.VehicleId,
                    OrderId = ReservationCartVM.OrderHeader.Id,
                    DailyRentalPrice = item.Price,
                    Count = item.Count
                };
                ReservationCartVM.OrderHeader.OrderTotal += orderDetails.Count * orderDetails.DailyRentalPrice;
                _unitOfWork.OrderDetails.Add(orderDetails);
            }
            //var payment = PaymentProcess(model);
            _unitOfWork.ReservationCart.RemoveRange(ReservationCartVM.ListCart);
            _unitOfWork.Save();
            HttpContext.Session.SetInt32(SD.rrReservationCart, 0);
            return RedirectToAction("KiralamaTamam");


        }

        //private Payment PaymentProcess(ReservationCartVM model)
        //{
        //    Options options = new Options();
        //    options.ApiKey = "sandbox-KHu3YiqNCVuaMvxMRSakpgUVaD5emU7J";
        //    options.SecretKey = "TZ1EqkSUeDomTIaqXSvvdHFE8cCQyBmq";
        //    options.BaseUrl = "https://sandbox-api.iyzipay.com";

        //    CreatePaymentRequest request = new CreatePaymentRequest();
        //    request.Locale = Locale.TR.ToString();
        //    request.ConversationId = new Random().Next(1111,9999).ToString();
        //    request.Price = model.OrderHeader.OrderTotal.ToString();
        //    request.PaidPrice = model.OrderHeader.OrderTotal.ToString();
        //    request.Currency = Currency.TRY.ToString();
        //    request.Installment = 1;
        //    request.BasketId = "B67832";
        //    request.PaymentChannel = PaymentChannel.WEB.ToString();
        //    request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

        //    //PaymentCard paymentCard = new PaymentCard();
        //    //paymentCard.CardHolderName = "John Doe";
        //    //paymentCard.CardNumber = "5528790000000008";
        //    //paymentCard.ExpireMonth = "12";
        //    //paymentCard.ExpireYear = "2030";
        //    //paymentCard.Cvc = "123";
        //    //paymentCard.RegisterCard = 0;
        //    //request.PaymentCard = paymentCard;

        //    PaymentCard paymentCard = new PaymentCard();
        //    paymentCard.CardHolderName = model.OrderHeader.CartName;
        //    paymentCard.CardNumber = model.OrderHeader.CartNumber;
        //    paymentCard.ExpireMonth = model.OrderHeader.ExpirationMonth;
        //    paymentCard.ExpireYear = model.OrderHeader.ExpirationYear;
        //    paymentCard.Cvc = model.OrderHeader.Cvc;
        //    paymentCard.RegisterCard = 0;
        //    request.PaymentCard = paymentCard;



        //    Buyer buyer = new Buyer();
        //    buyer.Id = model.OrderHeader.Id.ToString();
        //    buyer.Name = model.OrderHeader.Name;
        //    buyer.Surname = model.OrderHeader.Surname;
        //    buyer.GsmNumber = model.OrderHeader.PhoneNumber;
        //    buyer.Email = "email@email.com";
        //    buyer.IdentityNumber = "74300864791";
        //    buyer.LastLoginDate = "2015-10-05 12:43:35";
        //    buyer.RegistrationDate = "2013-04-21 15:12:09";
        //    buyer.RegistrationAddress = model.OrderHeader.Adres;
        //    buyer.Ip = "85.34.78.112";
        //    buyer.City = "Istanbul";
        //    buyer.Country = "Turkey";
        //    buyer.ZipCode = "34732";
        //    request.Buyer = buyer;

        //    Address shippingAddress = new Address();
        //    shippingAddress.ContactName = "Jane Doe";
        //    shippingAddress.City = "Istanbul";
        //    shippingAddress.Country = "Turkey";
        //    shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
        //    shippingAddress.ZipCode = "34742";
        //    request.ShippingAddress = shippingAddress;

        //    Address billingAddress = new Address();
        //    billingAddress.ContactName = "Jane Doe";
        //    billingAddress.City = "Istanbul";
        //    billingAddress.Country = "Turkey";
        //    billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
        //    billingAddress.ZipCode = "34742";
        //    request.BillingAddress = billingAddress;

        //    List<BasketItem> basketItems = new List<BasketItem>();
        //    var claimsIdentity = (ClaimsIdentity)User.Identity;
        //    var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        //    foreach (var item in _unitOfWork.ReservationCart.GetAll(i=>i.ApplicationUserId==claim.Value,
        //        includeProperties:"Vehicle"
        //        ))
        //    {
        //        basketItems.Add(new BasketItem()
        //        {

        //            Id = item.Id.ToString(),
        //            Name = item.Vehicle.CarName,
        //            Category1 = item.Vehicle.CategoryId.ToString(),
        //            ItemType=BasketItemType.PHYSICAL.ToString(),
        //            Price=(item.Count*item.Price).ToString()


        //        }) ;
        //    }
        //    request.BasketItems = basketItems;

        //    return Payment.Create(request, options);

        //}

      


        public IActionResult KiralamaTamam()
        {
            return View();
        }
        


        public IActionResult Plus(int cartId)
        {
            var cart = _unitOfWork.ReservationCart.GetFirstOrDefault(i => i.Id == cartId, includeProperties: "Vehicle");
            cart.Count += 1;
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
            var cart = _unitOfWork.ReservationCart.GetFirstOrDefault(i => i.Id == cartId, includeProperties: "Vehicle");
            if (cart.Count==1)
            {
                var count = _unitOfWork.ReservationCart.GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count();
                _unitOfWork.ReservationCart.Remove(cart);
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(SD.rrReservationCart, count - 1);
            }
            else
            {
                cart.Count -= 1;
                _unitOfWork.Save();
            }
          
            return RedirectToAction(nameof(Index));
        }



        public IActionResult Remove(int cartId)
        {
            var cart = _unitOfWork.ReservationCart.GetFirstOrDefault(i => i.Id == cartId, includeProperties: "Vehicle");
           
                var count = _unitOfWork.ReservationCart.GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count();
                _unitOfWork.ReservationCart.Remove(cart);
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(SD.rrReservationCart, count - 1);
            
            return RedirectToAction(nameof(Index));
        }



    }
}
