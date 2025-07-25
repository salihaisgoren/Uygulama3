

## ⚙️ Gereksinimler

- .NET 6.0 veya üzeri  
- Newtonsoft.Json (JsonConvert için)  
- API'nin `https://localhost:7170/api/` adresinde çalışıyor olması  

---

## 📁 Proje Yapısı

- `Program.cs` — Konsol uygulaması, kullanıcıdan veri alır ve API çağrılarını yapar.  
- DTO sınıfları (`PokemonDto`, `OwnerDto` vb.) — API'den alınan ve gönderilen verilerin yapısını temsil eder.  
- HTTP istekleri `HttpClient` kullanılarak yapılır.  

---

## ⚠️ Dikkat Edilmesi Gerekenler

- API adresi ve endpoint yapısı `https://localhost:7170/api/` olarak ayarlanmıştır.  
  Gerekirse kendi API adresinize göre güncelleyin.  
- Tarih formatı `yyyy-MM-dd` şeklinde olmalıdır.  
- PUT ve POST işlemlerinde ilgili veriler eksiksiz ve doğru formatta girilmelidir.  
- `ServerCertificateCustomValidationCallback` ile sertifika doğrulaması bypass edilmiştir.  

 
