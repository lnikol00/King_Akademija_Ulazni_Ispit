# King_Akademija_Ulazni_Ispit
KING ICT Akademija - Ljetna praksa - Ulazni ispit (.NET)

## Sadržaj
* [Zadatak](#zadatak)
* [Kloniranje i pokretanje projekta](#kloniranje-i-pokretanje-projekta)
* [Opis endpointa](#opis-endpointa)
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

## Opis endpointa

## Konfiguracija baze podataka
