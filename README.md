# ShoppingCenter

# ENGLISH

## This project is developed for an assignment and consists of these technologies and approches below:

### Technologies & Approaches
- Mediator pattern
- Repository pattern
- TDD (for product and cart)
- Fluent Assertions
- Fluent Validations
- MongoDB - InMemory DB support (just change the value of UseMongoDb in the appsettings.json file)
- OOP and SOLID principals
- .NetCore 2.2 WebApi
- Bogus moq data generation for Product (FakeProductService)
- Auto service injection by Core.DependencyInjection Library which has several implementation by myself
- ...

### Features
- Double stock check on add cart and on check out
- Price check on check out
- Product stock quantity update on check out
- ...

### Usage 
This application has two different working states. First one is, development & testing mode. If you use IIS for debug and if you change the ASPNETCORE_ENVIRONMENT variable as Production on web.config, you will change the application behaviour. In development mode, FakeProductService will be initialized with 100 products that will be generated randomly by bogus with quantity&price. In Production mode, ProductService will be injected with no product.
And you can change in memory db & MongoDB on appsettings.json file
The application will be worked in development & test mode default.

# TURKISH

## Bu proje aşağıdaki teknoloji ve yaklaşımları içeren bir assignment için geliştirilmiştir.

### Teknolojiler & Yaklaşımlar
- Mediator pattern
- Repository pattern
- TDD (ürün ve sepet için)
- Fluent Assertions
- Fluent Validations
- MongoDB - InMemory DB desteği (appsettings.json dosyasındaki UseMongoDb alanını true yapabilirsiniz)
- OOP ve SOLID prensipleri
- .NetCore 2.2 WebApi
- Bogus ile ürün moq data oluşturulması(FakeProductService)
- Tarafımca eklentileri olan Core.DependencyInjection kütüphanesi sayesinde otomatik servis inject edilmesi
- ...

### Features
- Sepete ürün eklemede ve satın alma sırasında çift ürün sayısı kontrolü
- Satın alma sırasında fiyat kontrolü
- Satın alma sonrası kalan ürün sayısı güncelleme
- ...

### Usage
Bu uygulamanın iki farklı davranış şekli vardır. Birincisi geliştirme ve test amaçlı. Eğer IIS'te çalıştırırken, web.config dosyasındaki ASPNETCORE_ENVIRONMENT değişkeni Production olarak güncellenirse, uygulamanın davranışı değişir. Geliştirme modunda, FakeProductService servisi Bogus tarafından fiyat ve ürün sayısı random olarak üretilen 100 adet ürünle initialize olur. Production modunda ise, ProductService inject edilir ve 0 ürüne sahip olur. 
Ve MongoDB kullanmak için, appsettings.json dosyasındaki UseMongoDb alanını true yapabilirsiniz.
Defaultta uygulama geliştirme ve test modunda çalışacaktır.
