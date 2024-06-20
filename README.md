# King_Akademija_Ulazni_Ispit
KING ICT Akademija - Ljetna praksa - Ulazni ispit (.NET)

## Sadržaj
* [Zadatak](#zadatak)
* [Kloniranje i pokretanje projekta](#kloniranje-i-pokretanje-projekta)
* [Autorizacija i autentifikacija](#autorizacija-i-autentifikacija)
* [Konfiguracija baze podataka](#konfiguracija-baze-podataka)

## Zadatak
Potrebno je razviti middleware koje će imati mogućnost dohvata proizvoda iz različitih izvora (web
servisi, baze podataka, file system, RSS). Kako ste tek počeli s projektom dobili ste samo prvi izvor
podataka, a to je REST API koji se nalazi na dnu dokumenta. Vaš zadatak je razviti middleware REST
API koja prikazuje proizvode s naprednim mogućnostima filtriranja. Imajte na umu da u budućnosti
morate moći dodati i druge izvore proizvoda. 

**Zadatak**
* Implementirajte endpoint koji vraća listu proizvoda (slika, naziv, cijena, skraćen opis do 100
znakova)
* Implementirajte endpoint koji vraća detalje jednog proizvoda
* Implementirajte endpoint koji omogućava filtriranje po kategoriji i cijeni
* Implementirajte endpoint koji za uneseni tekst pretražuje proizvode po nazivu

**"Nice to have"**
* Autentifikacija i autorizacija
* Razmislite kako biste riješili ako korisnik više puta poziva endpoint pretrage ili filtriranja proizvoda s istim parametrima
* Logiranje (npr. Info, warning, error…)
* Testovi (npr. Integracijski, unit…) 

**Dodatni zahtjevi**
* Obratiti pozornost na strukturu projekta i korištenje preporučenih dobrih praksi i konvencija
* Dokumentirati endpointove
* Rješenje postavite na jednu od platformi koja podržava GIT (Github, Bitbucket, GitLab…) s pregledom commiteva da vidimo kako ste napredovali u vašem radu
* Uključite readme file s uputama kako bi mogli rješenje podići lokalno:
* Konfiguracija i podešavanje baze, servisa
* Postaviti token ili testnog korisnika, ako koristite autentifikaciju i autorizaciju
* Ako koristite docker, upute za pokretanje putem dockera
* Opisati kako testirati aplikaciju

Za API poziv možete koristiti [https://dummyjson.com](#https://dummyjson.com) i to za:
* Podaci o korisnicima sustava - [https://dummyjson.com/users](#https://dummyjson.com/users)
* Prijava u sustav i dohvat tokena - [https://dummyjson.com/docs/auth](#https://dummyjson.com/docs/auth)
* Proizvode - [https://dummyjson.com/products](#https://dummyjson.com/products)
* Kategorije - [https://dummyjson.com/products/categories](#https://dummyjson.com/products/categories)

## Kloniranje i pokretanje projekta

Git naredba git clone koristi se za stvaranje kopije (kloniranja) postojećeg Git repozitorija iz udaljenog izvora na lokalno računalo. To znači da ćeš preuzeti sve datoteke, povijest commit-ova, grane i oznake (tags) iz udaljenog repozitorija.
U terminalu ili komandnom prozoru pokreni naredbu

```
git clone git@github.com:lnikol00/King_Akademija_Ulazni_Ispit.git
```

Nakon što je kloniranje završeno, možeš ući u direktorij repozitorija:

```
cd King_Akademija_Ulazni_Ispit
```

Za pokretanje aplikacije potrebno je u direktoriju, u terminalu pokrenuti naredbu:

```
dotnet run
```

## Autorizacija i autentifikacija

## Konfiguracija baze podataka

Bazu podataka u projektu koristimo za slučaje pretrage po nazivu i filtriranja po cijeni i kategoriji. Svaka pretraga koju korisnik radi sprema se u bazu podataka, te ako korisnik pretažuje po istim parametrima naš servis poziva pretragu spremljenu u bazi podataka. Ovakav pristup omogućuje pružateljima usluge da prate česte pretrage koje korisnici pretražuju. Na taj način, primjerice, može se pripremiti nova tablica u bazi podataka gdje ce biti spremljeni podaci najčešće pretrage, tako da se smanji potreba komuniciranja sa vanjskim API-jem.

U ovom projektu za bazu podataka korišten je SQL server. Za omogućeno korištenje potrebno je konfiuirati dependency injection u Program.cs

```js
builder.Services.AddDbContext<DatabaseContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
```

**builder.Services.AddDbContext<DatabaseContext>** - Kada se DatabaseContext traži u aplikaciji (npr. u kontroleru ili servisu), DI kontejner će ga automatski kreirati i pružiti.
**options.UseSqlServer** - Ovdje se konfigurira DatabaseContext da koristi SQL Server kao bazu podataka.
**builder.Configuration.GetConnectionString("Default")** - Ova metoda dohvaća connection string za SQL Server iz konfiguracijskih postavki aplikacije.

Primjer konfiguracije u appsettings.json:

```js
"ConnectionStrings": {
  "Default": "Data Source=your_server_name;Database=your_database_name;TrustServerCertificate=True;Integrated Security=True"
},
```

Za omogućeno korištenje baze podataka u servisima, kreirana je klasa koja se zove DatabaseContext.

```js
public class DatabaseContext : DbContext
```
Ova klasa nasljeđuje klasu DbContext iz Entity Framework Core-a. DbContext je glavna klasa za interakciju sa bazom podataka.

```js
public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
{
}
```

Ovaj konstruktor prima DbContextOptions objekt i prosljeđuje ga bazi koristeći base(options), odnosno omogućuje kongiguraciju konteksta baze podataka (postavljanje veze na bazu podataka).

```js
public DbSet<SearchParams> Searches { get; set; }
public DbSet<FilterParams> Filters { get; set; }
```

DbSet je glavni način za izvršavanje upita i rad s instancama entiteta. 
SearchParams i FilterParams su klase koja definiraju model podataka (entitet) za pohranu u bazi podataka.

```js
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<SearchParams>().HasKey(nameof(SearchParams.Search));
    modelBuilder.Entity<FilterParams>().HasKey(nameof(FilterParams.Order), nameof(FilterParams.Category));

    base.OnModelCreating(modelBuilder);
}
```

Ova metoda se koristi za kofniguraciju modela entiteta koristeći ModelBuilder. Odnosno omogućuje nam dodatne konfiguracije koje nisu moguće putem atributa.
U ovoj metodi definira se da kombinacija danih vrijednosti mora biti jedinstvena za svaki unos u tablicu. Odnosno svaki Search iz modela SearchParams mora biti jedinstven, te svaki Order i Category iz FilterParams modela mora biti jedinstven u tablici.
