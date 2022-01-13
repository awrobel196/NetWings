# Dokumentacja NetWings ![alt text](https://img.shields.io/badge/Aplikacja-1.0-blue) ![alt text](https://img.shields.io/badge/Dokumentacja-1.2-green)
![Netwings logo](https://github.com/awrobel196/NetWings/blob/main/src/WebUI/wwwroot/assets/logo-dark.png#gh-light-mode-only)
![Netwings logo](https://github.com/awrobel196/NetWings/blob/main/src/WebUI/wwwroot/assets/logo-white.png#gh-dark-mode-only)

## Netwings
#### Demo
Demo aplikacji dostępne pod adresem: https://mystifying-merkle.188-34-164-7.plesk.page/

#### O aplikacji
NetWings to aplikacja webowa pozwalająca na monitoring stron internetowych pod kątem ich dostępności, jakości oraz bezpieczeństwa. Platforma agreguje dane pobierane przy użyciu publicznych API i w prosty spoób przedstawia je użytkownikowi. Głównym mechanizmem platformy jest monitorowanie staturu `up` oraz `down` dodanej przez użytkownika strony internetowej. Wszelkie zmiany w tym zakresie zgłaszane są użytkownikowy za pomocą bramki SMS. 

#### Użytkownicy
Aby skorzystać z platformy użytkownik musi założyć na niej konto podając imię, nazwisko, adres email, hasło oraz numer telefonu (na który przesyłane będą wiadomości sms dotyczące dostępności strony). Użytkownik po zarejestrowaniu nowego konta może dodać strony które będą monitorowane przez system, a uzyskane wyniki przedstawiane zostaną danemu użytkownikowi. W ramach swojego konta, użytkownik może wygenerować link do rejestracji, przy użyciu którego będzie możliwość utworzenia nowego konta i dostęp do tych samych stron co dodane na koncie w którym wygenerowano wyżej wymieniony link. 

## Infrastruktura platformy
Platforma NetWings składa się z dwóch głownych modułów:
- `src` -> część przechowująca aplikację Webową wraz z warstwą dostępu do danych
- `machines` -> część przechowywująca maszyny monitorujące dodane strony internetowe

#### Machines
Na część maszyn składają się 4 instancje Azure Function, które odpowiadają za monitoring i aktualizację stanu stron dodanych do platformy. Na maszyny składają się następujące instancje:
- `BenchmarkMachineRoot` -> maszyna uruchamiana co 20 minut, której zadaniem jest sprawdzenie czy wszystkie strony posiadają aktualny raport wyniku benchmark przeprowadzonego za pomocą GTMetrix
- `BenchmarkMachineWorker` -> maszyna uruchamiana codziennie o 23:20, której zadaniem jest otworzenie każdego z raportu zapisanego przez `BenchmarkMachineRoot` i odczytanie wyniku każdej ze stron
- `UptimeMachineRoot` -> maszyna uruchamiana co minutę, jej zadaniem jest pobranie wszystkich stron z bazy i rozpoczęcie testów Uptime
- `UptimeMachineWorker` -> maszyna przy pomocy wywołania HTTP przez `UptimeMachineRoot`, jej zadanie to wyslanie żądania do otrzymanej strony internetowej i zapisanie wyniku w bazie dotyczącego dostępności danej strony


## Mechanizm logowania
Strona logowania zwracana jest przez kontroler `LoginController`w akcji `Index`. W zależności od źródła, które prowadzi do strony logowania jako parametry adresu przekazywane mogą być różne wartości parametru `type`:
- `register` -> parametr przyjmowany w przypadku przekierowania do strony logowania po pomyślnej rejestracji
- `logout` -> parametr przyjmowany w przypadku przekierowania do strony logowania po pomyślnym wylogowaniu


Do pobierania wartości parametrów z paska adresu służy funkcja `getParameterByName("{nazwa_parametru}")`.

Parametry ze stron źródłowych przekierowywujące do logowania mogą zostać przekazane na kilka sposobów. W aplikacji **najczęściej** wykorzystywany jest do tego kod:
```
return RedirectToAction("Index","Login",new { type = "logout" });
```
W zależności od przekazanych parametrów w oknie logowania wyświetlane są stosowne komunikaty, które posiadają klasę `display-none` a w zależności od stanu aplikacji klasy tej nie posiadają.

W momencie kliknięcia przycisku **Zaloguj** wywoływana jest akcja `TryLogin` z kontrolera `LoginController`. Akcja ta wykonuje metodę `TryLogin` z repozytorium `ILoginRepository`, która najpierw sprawdza czy podany użytkownik istnieje w bazie, a jeśli tak przekierowywuje do metody `Login` gdzie tworzony jest kontekst użytkownika. Jeśli kontekst zostanie utworzony w poprawny sposób użytkonikowi wyświetlony zostanie stosowny komunikat, a następnie przekierowany zostanie do strony głównej aplikacji.

## Mechanizm rejestracji
Użytkownik w serwisie może:
- założyć konto tworząc nową licencję
- założyć konto dołączająć do istniejącej licencji


### Rejestracja z utworzeniem nowej licencji
Strona rejestracji wywoływana jest przez akcję `Index` z kontrolera `RegisterController` bez przekazania jakichkolwiek parametrów. Użytkownik w czasie rejestracji musi uzupełnić pola:
- Adres email -> walidacja zgodności z formą adresu email
- Imię i nazwisko -> walidacja czy pole nie jest puste
- Telefon -> walidacja zgodności z formą adresu email
- Hasło -> walidacja pod kątem długości ciągu minimum 8 znaków

W momencie naciśnięcia na przycisk **Zarejestruj się** wywoływana jest metoda `tryRegister()` ktora najpierw pobiera wartości z pól formularza, a następnie waliduje je pod kątem poprawności wprowadzonych ciągów. Jeśli jakiekolwiek z pól nie zostanie poprawnie zwalidowane, wyświetlony zostanie komunikat informujący o popełnionym błędzie. Walidacja każdego z pól powoduje jedną z dwóch akcji:
- Wyświetlenie komunikatu o błędzie -> w przypadku blędnego ciągu
- Ukrycie komunikatu o błędzie (jeśli wyświetlony) -> w przypadku poprawnego ciągu

Jeśli wszystkie z pól zostaną zwalidowane poprawnie, dane pobrane z pól formularza zostaną przekazane do akcji `TryRegister` kontrolera `RegisterController`. Kolejno akcja przekaże otrzymane dane do metody `TryRegister` repozytorium `IRegisterRepository`. W pierwszej kolejności za pomocą repozytorium `ILicenseRepository` i jego metody `CreateLicense` utworzona zostanie nowa Licencja, która przypisana zostanie do tworzonego użytkownika. W kolejnym kroku zmapowane zostaną pola użytkownika i dodany zostanie on do bazy danych. Jeśli operacja dodawania użytkownika przebiegła pomyślnie, użytkownik zostanie przekierowany do strony Logowania z parametrem `register` co pozwoli na wyświetlenie komunikatu o poprawnie wykonanej operacji rejestracji nowego konta.

### Rejestracja z dołączeniem do istniejącej licencji
Strona rejestracji wywoływana jest przez akcję `Index` z kontrolera `RegisterController`z przekazanym parametrem typu `invite`. Parametr w swojej wartości zawiera klucz, który pozwoli założyć noe konto oraz dołączyć do istniejącej licencji. Sam proces walidacji danych wygląda bliźniaczo co w przypadku **Rejestracji z utworzeniem nowej licencji** jedyną kluczową zmianą jest wywołanie przez metodę `TryRegister` kontrolera `RegisterController` metody `TryRegisterInExistLicense` zamiast `TryRegister`. W metodzie `TryRegisterInExistLicense` repozytorium `IRegisterRepository` w pierwszej kolejności sprawdzana jest wartość parametru `invite`. Jesli jest ona zgodna z identyfikatorem istniejącej już licencji, pola użytkownika zostaną zmapowane do nowego obiektu typu `User` włącznie z pobraną licencją. Po utworzeniu nowego użytkownika, analogicznie co w przypadku **Rejestracji z utworzeniem nowej licencji** użytkownik przekierowany zostanie do strony Logowania z parametrem `register` co pozwoli na wyświetlenie komunikatu o poprawnie wykonanej operacji rejestracji nowego konta. 


## Pobieranie danych obecnie zalogowanego użytkownika
Pobieranie danych obsługiwane jest przez statyczną klasę `UserClaimService`, która posiada metody zwracające: 
- Identyfikator użytkownika (`Guid`) -> metoda `GetUserId`
- Identyfikator licencji (`Guid`) -> metoda `GetLicenseId`
- Nazwę użytkownika (`String`) -> metoda `GetName`
- Email użytkownika (`String`) -> metoda `GetEmail`


Wszystkie metody jako parametr przyjmują obiekt typu `ClaimsPrincipal`. 

#### Przykładowe wywołanie metody
```
public class UptimeBenchmarkController : Controller
{
  public async Task<IActionResult> Index()
  {
    var userName = UserClaimsService.GetName(User);
    return View(userName);
  }
}
```

## Hosting platformy

#### Aplikacja webowa  - 🐳 Docker  + Plesk Linux
Aplikacja webowa hostowana jest przy pomocy serwera Linux wyposażonego w panel administracyjny Plesk. Aby wykonać poniższe kroki, wymagane jest posiadanie zainstalowanego środowiska Plesk oraz rozszerzenia do panelu administracyjnego plesk o nazwie Docker. Aby uruchomić aplikację należy:

1. Otwórz konsolę w katalogu głównym (NetWings)
2. Zbuduj nowy obraz docker `docker build -t webui .`
3. Otaguj zbudowany obraz `docker tag webui:latest netwings/webui:0.0.1`
4. Zapisz zbudowany obraz jako archiwum w formacie .tar `docker save netwings/webui -o netwingsportal.tar
5. Wyślij zbudowane archiwum w formacie .tar na serwer VPS
6. Połącz się z VPS przy użyciu SSH i przejdź do lokalizacji gdzie zostało przesłane archiwum z obrazem
7. Załaduj obraz docker używając komendy `docker load -i netwingsportal.tar`
8. Zaloguj się do panelu Plesk, przejdź do zakładki Docker uruchom nowy kontener z obrazu Netwingsportal wybierając jego aktualną wersję
9. W czasie tworzenia kontenera odznacz opcję *Automatic port mapping*
10. Po zbudowaniu kontenera wejdź w zakładkę *Settings* danego kontenera i w sekcji *Manual mapping* wpisz port pod którym dostępna będzie aplikacja
11. Przejdź w zakładkę Websites & Domains -> [Domena] -> Docker proxy Ruler i wybierz dodany kontener

