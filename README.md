# 🚗 RentCar Projesi

**RentCar**, araç kiralama işlemlerini yönetmek için geliştirilmiş bir web uygulamasıdır.  
Bu proje, bir veya birden fazla şirketin araç kiralama ve rezervasyon süreçlerini web tabanlı olarak yönetmelerine olanak tanır.

---

## 🎯 Proje Amacı

Bu projenin amacı, şirket yöneticileri ve çalışanlarının sisteme giriş yaparak müşterilerin bilgilerini ve kiraladıkları araçları yönetebilmelerini sağlamaktır.  
Ayrıca, müşteriler de web üzerinden mevcut ve kiralanabilir araçları görüntüleyebilir, rezervasyon veya kiralama taleplerinde bulunabilirler.

Şirket yöneticileri/çalışanları sisteme giriş yaparak sistemi kullanabilmelidir yani sistemde kullanıcılar ayrı birer varlıktır. Kullanıcılar müşterilerin özlük bilgilerini ve kiraladığı araç bilgilerini girerek, aracın kiralandığı süre boyunca kiralanmaması ve takibi için gerekli işlemleri yapmalıdır. Yöneticiler veya çalışanlar sisteme web uygulamasından girebilmeli ve kiralama yapabilmelidir. Müşteriler ise şirketin hazırda ve kiralanabilir araçlarını web üzerinden görüntüleyebilir hatta rezervasyon ya da kiralama isteği başlatabilir.(adet girdim kiralanabilirler gözükecek,müşteri üye olup giriş yapacak ) Kullanıcı ve çalışanların şifre bilgileri geliştiricilerden saklanmalıdır.

Şirketler bilgi olarak; şirket adı, şehir, adres, araç sayısı, şirket puanı gibi bilgiler içerebilir.

Araç bilgileri olarak; araç adı, modeli, gereken ehliyet yaşı, minimum yaş sınırı, günlük sınır km’si, aracın kendi anlık km’si, airbag, bagaj hacmi, koltuk sayısı, günlük kiralık fiyatı gibi değerleri barındırabilir.

Kiralık bilgileri olarak; kiralanmış araçların kimlere hangi zaman aralığında kiralandığı, veriliş km’si, son kilometre, alınan ücret miktarını bulundurur.

Ayrıca müşterilerin özlük bilgileri (ad, soyad vb.) bulunur.

---

## 🛠️ Kullanılan Teknolojiler

- **Backend**: ASP.NET Core MVC 5
- **Mimari**: N Katmanlı Mimari
- **Desenler**: Generic Repository, Unit of Work

---

## 📂 Proje Yapısı
Fonksiyonel Olmayan Gereksinimler
Sistem geliştirilirken sizlerden beklenen fonksiyonel olmayan teknik gereksinimler aşağıdaki gibidir:
•	N-Katmanlı Mimari 
•	ORM kullanımı (opsiyonel) 
•	Repository ve/veya Unit Of Work desenlerinin kullanılması 
•	SOLID Prensiplerine uyum 
•	Geleneksel veya NOSQL 
•	Github entegrasyonu ve düzenli commit  
Proje aşağıdaki ana dosya ve klasörleri içerir:

- `RentCarProject/`: Uygulamanın ana kaynak kodlarını içerir.
- `.gitattributes`: Git özniteliklerini tanımlar.
- `Projescript.sql`: Veritabanı oluşturma ve başlangıç verilerini içeren SQL betiği.
- `README.md`: Proje hakkında bilgi veren bu dosya.

---

## 🚀 Kurulum ve Çalıştırma

1. **Depoyu Klonlayın**:
   ```bash
   git clone https://github.com/Sarizeybekk/RentCar.git

 
 
 
 Area kullanımı:
 ![Untitled Diagram drawio (1)](https://user-images.githubusercontent.com/85437211/148270798-7dff0596-b780-441c-b264-9a380e026a91.png)

![image](https://user-images.githubusercontent.com/85437211/148270503-5f702f2a-904e-44a5-9762-49e7ee3d5c49.png)

![image](https://user-images.githubusercontent.com/85437211/148450815-c7045027-70d8-427b-94a3-f4a90d995bab.png)



