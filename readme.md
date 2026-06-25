# Sorelia – platformă e-commerce pentru produse cosmetice și de sănătate

## Descriere proiect

Sorelia este o aplicație web dezvoltată în ASP.NET Core Razor Pages pentru gestionarea unui magazin online de produse cosmetice și de sănătate. Aplicația permite afișarea produselor, autentificarea utilizatorilor, gestionarea listei de favorite, utilizarea coșului de cumpărături, plasarea comenzilor și administrarea produselor, comenzilor și stocurilor.

## Tehnologii utilizate

* ASP.NET Core Razor Pages
* Entity Framework Core
* ASP.NET Core Identity
* SQLite
* Bootstrap
* Visual Studio 2022

## Versiune .NET necesară

Proiectul a fost dezvoltat și testat cu: .NET 9 SDK
Pentru rulare este necesară instalarea .NET 9 SDK sau deschiderea proiectului în Visual Studio 2022 cu suport pentru ASP.NET Core.

NOTA!!! Din motive de securitate, arhiva predată nu conține un fișier appsettings.json configurat și nu include parole reale pentru serviciul de e-mail.
        Pentru rularea aplicației, fișierul appsettings.example.json trebuie copiat și redenumit în appsettings.json.
        
## Pornirea proiectului

1. Dezarhivați proiectul.
2. Deschideți fișierul: BeautyHealthStore.sln în Visual Studio 2022.
3. Așteptați restaurarea automată a pachetelor NuGet.
4. Selectați proiectul `BeautyHealthStore` ca proiect de pornire.
5. Rulați aplicația folosind butonul **Start** sau tasta **F5**.

Aplicația va porni local în browser.

## Baza de date

Aplicația folosește baza de date SQLite: sorelia.db

Proiectul poate fi rulat direct folosind fișierul sorelia.db inclus în arhivă.

Migrațiile Entity Framework Core sunt păstrate în folderul Migrations și pot fi utilizate pentru reconstruirea bazei de date dacă fișierul sorelia.db nu este disponibil.


## Cont administrator pentru test

Aplicația creează automat rolurile necesare și contul de administrator la pornire, dacă acestea nu există deja în baza de date.

Cont administrator:

E-mail: test@test.ro
Parolă: Test123!

Acest cont de test poate fi folosit pentru accesarea zonei de administrare a aplicației. Cont special facut doar pentru test.

## Configurare e-mail

Aplicația include o funcționalitate pentru trimiterea automată a e-mailurilor de confirmare după plasarea unei comenzi.

Din motive de securitate, arhiva predată nu conține parola reală SMTP/Gmail.

În fișierul: appsettings.example.json secțiunea pentru e-mail este configurată local astfel:

"EmailSettings": {
  "SmtpServer": "smtp.gmail.com",
  "SmtpPort": 587,
  "SenderEmail": "adresa_mea@gmail.com",
  "SenderPassword": "se_configureaza_local"
}


Pentru testarea trimiterii e-mailurilor, valoarea `SenderPassword` trebuie înlocuită local cu o parolă de aplicație validă generată din contul Gmail.


## Autor

Poptile Darius Ionel
Specializarea Informatică Economică
Facultatea de Științe Economice și Gestiunea Afacerilor
Universitatea Babeș-Bolyai
