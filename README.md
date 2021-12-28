# Dokumentacja NetWings ![alt text](https://img.shields.io/badge/Wersja-1.0-green)

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


