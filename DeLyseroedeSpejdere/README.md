# De Lyserøde Spejdere

**2. semester – Datamatiker | ASP.NET Core Razor Pages, C# og MySQL**

Et webbaseret aktivitets- og tilmeldingssystem udviklet til skolecasen *De Lyserøde Spejdere*, som omhandler en spejderlejr i Hedeland ved Roskilde.

Systemet giver ledere mulighed for at administrere aktiviteter og spejdere mulighed for at tilmelde og framelde sig aktiviteter.

**Projektperiode:** 19.–30. oktober 2026  
**Udviklingsform:** Individuelt projekt med SCRUM-inspireret planlægning  
**Repository:** https://github.com/martinsir/DeLyseroedeSpejdere

---

## 1. Teknologier

- C# og .NET 10
- ASP.NET Core Razor Pages
- MySQL og ADO.NET med MySqlConnector
- Repository Pattern og dependency injection
- Cookie Authentication og rollebaseret autorisation
- HTML, CSS og JavaScript
- xUnit til automatiserede tests
- Git og GitHub til versionsstyring

## 2. Funktionalitet

### Offentlig bruger

- Se tilgængelige aktiviteter.
- Se aktiviteternes oplysninger og ledige pladser.
- Logge ind i systemet.

### Spejder

- Se aktiviteter.
- Tilmelde sig aktiviteter.
- Se egne tilmeldinger.
- Framelde sig aktiviteter.
- Se, når en aktivitet allerede er tilmeldt.

Tilmeldinger kontrolleres ud fra:

- Minimumsalder.
- Krav om færdighedstegn.
- Antal ledige pladser.
- Eksisterende tilmelding.

### Leder

- Oprette aktiviteter.
- Redigere aktiviteter.
- Slette aktiviteter.
- Se deltagerlister.

Oprettelses- og redigeringsformularerne benytter separate dato- og tidsfelter med 24-timers klokkeslæt.

### Sikkerhed og adgangskontrol

Login håndteres med ASP.NET Core Authentication Cookies og rollebaserede claims.

Lederfunktionerne er beskyttet på serversiden, så spejdere ikke kan tilgå dem ved at indtaste URL'en direkte.

SQL-forespørgsler anvender parametre for at reducere risikoen for SQL-injection.

## 3. Installation og opstart

### Krav

- Visual Studio med understøttelse af ASP.NET Core og .NET 10.
- .NET 10 SDK.
- MySQL Server.
- MySQL Workbench eller et tilsvarende databaseværktøj.

### Trin 1 – Opret databasen

Åbn `Data/schema.sql` i MySQL Workbench, og kør hele scriptet.

Scriptet opretter databasen `LyseroedeSpejdere` samt følgende tabeller:

| Tabel | Formål |
|---|---|
| Users | Gemmer brugere, roller, alder og færdighedstegn |
| Activities | Gemmer aktiviteter og deres krav |
| Registrations | Forbinder brugere med aktiviteter |

Databasen anvender primærnøgler, fremmednøgler og en sammensat primærnøgle i `Registrations`.

### Trin 2 – Konfigurer databaseforbindelsen

Kopiér `appsettings.json` til `appsettings.Development.json`.

Ret `Password=CHANGE_ME` i forbindelsesstrengen til adgangskoden for din lokale MySQL-server.

`appsettings.Development.json` er udeladt fra Git, så lokale databaseadgangskoder ikke skal uploades til GitHub.

ASP.NET Core User Secrets kan anvendes som alternativ.

### Trin 3 – Start applikationen

1. Åbn `DeLyseroedeSpejdere.sln` i Visual Studio.
2. Vent på, at NuGet-pakkerne indlæses.
3. Vælg webprojektet som startup-projekt.
4. Start med F5 og HTTPS-profilen.
5. Åbn den lokale adresse, som Visual Studio viser.

Ved første start kan applikationen oprette demo-brugere og demo-aktiviteter, når `DemoUsers:Enabled` er sat til `true`.

## 4. Demo-brugere

Følgende brugere anvendes kun til lokal demonstration og test.

| Rolle | Brugernavn | Adgangskode |
|---|---|---|
| Leder | `leder` | `Lejr2026!` |
| Spejder, 13 år | `spejder` | `Lejr2026!` |
| Spejder, 10 år | `junior` | `Lejr2026!` |

Adgangskoderne lagres som hashes.

**Bemærk:** Demo-brugere og deres kendte adgangskoder er kun beregnet til skoleprojektet. Demo-seeding bør deaktiveres, og adgangsoplysningerne ændres før eventuel rigtig drift.

## 5. Projektstruktur og arkitektur

| Mappe eller fil | Ansvar |
|---|---|
| `Models/` | Domæneklasser og validering |
| `Repositories/` | Databaseadgang og SQL-operationer |
| `Pages/` | Razor Pages og PageModels |
| `Services/` | Services og oprettelse af demodata |
| `Data/schema.sql` | MySQL-databasestruktur |
| `Docs/` | Projekt-, design- og testdokumentation |
| `Tests/` | Automatiserede xUnit-tests |
| `Program.cs` | Konfiguration og dependency injection |

Projektet benytter følgende repository-interfaces:

- `IActivityRepository`
- `IRegistrationRepository`
- `IUserRepository`

Disse adskiller databaseoperationer fra Razor Pages og registreres gennem dependency injection.

`RegistrationRepository.JoinAsync` anvender en databasetransaktion med `FOR UPDATE` ved kapacitetskontrol. Tabellen `Registrations` har desuden en sammensat primærnøgle, der forhindrer identiske tilmeldingsrækker.

## 6. Test og kvalitetssikring

### Automatiserede tests

Projektet indeholder et separat xUnit-testprojekt.

**Seneste kontrollerede resultat: 27. oktober 2026**

| Testområde | Resultat |
|---|---|
| ActivityValidationTests | 5 bestået |
| RegistrationRulesTests | 4 bestået |
| I alt | 9/9 bestået |

Visual Studio gennemførte desuden build uden fejl.

Tests kan køres via Visual Studio under **Test → Run All Tests** eller med kommandoen `dotnet test DeLyseroedeSpejdere.sln`.

### Manuel funktionstest

Følgende funktioner er afprøvet:

- Login som leder og spejder.
- Rollebaseret adgangskontrol.
- Oprettelse, redigering og sletning af aktiviteter.
- Tilmelding og framelding.
- Visning af egne tilmeldinger.
- Visning af deltagerlister.
- Afvisning ved utilstrækkelig alder.
- Afvisning ved manglende færdighedstegn.
- Validering af start- og sluttidspunkter.

Databasens sammensatte primærnøgle er testet mod dubletregistrering. Et fuldt gentaget tilmeldingsforløb gennem webapplikationen mangler særskilt afprøvning.

### Brugertest

Systemet er afprøvet af tre testpersoner.

Brugertestene identificerede et fælles problem med AM/PM-format ved dato- og tidsindtastning.

Dette forbedringspunkt, **UX-01**, blev rettet ved at adskille dato og klokkeslæt og indføre eksplicit 24-timersformat i både oprettelses- og redigeringsformularerne.

Rettelsen er efterfølgende afprøvet lokalt. Der er ikke dokumenteret en ny brugertest med de samme tre personer efter rettelsen.

### Fejlrettelser

| ID | Problem | Status |
|---|---|---|
| CR-01 | Valgfrit færdighedstegn blev behandlet som påkrævet | Rettet og testet |
| CR-02 | Tilmeldingsknappen viste ikke korrekt status efter tilmelding | Rettet og testet |
| UX-01 | AM/PM skabte forvirring i dato- og tidsfelter | Rettet og lokalt testet |

## 7. SCRUM og Definition of Done

Projektet er planlagt med user stories, story points og to sprintperioder inden for den officielle projektperiode.

SCRUM-planen er tilrettelagt til et individuelt skoleprojekt. De beskrevne SCRUM-events er en planlægningsmodel og ikke dokumentation for faktisk afholdte gruppemøder.

### Product Backlog

- US-01 til US-08: Funktionelt implementeret.
- US-09: Flere ledere pr. aktivitet – ikke implementeret.

Samlet estimering: **31 story points**, heraf **26 story points funktionelt implementeret**.

At en user story er implementeret betyder ikke automatisk, at alle Definition of Done-krav er opfyldt.

### Definition of Done

Projektets DoD omfatter ni krav, herunder Repository Pattern, Nielsen-heuristikker, Gestalt, interfaces, klassediagram, Code Review, versionsstyring, brugertest og unit tests.

Den opdaterede status fremgår af dokumentationen i `Docs/`.

**Uafhængig Code Review-godkendelse afventer fortsat.**

## 8. Dokumentation

Mappen `Docs/` indeholder blandt andet:

| Dokument | Beskrivelse |
|---|---|
| `SCRUM_Projektplan.docx` | Projektplan, sprintmål og status |
| `DeLyseroedeSpejdere_SCRUM_Backlog_2026-10-27.xlsx` | Product Backlog, tests og DoD-status |
| `User_Stories_og_Acceptkriterier.docx` | User stories og acceptkriterier |
| `DoD_Checkliste.md` | Definition of Done-kontrol |
| `DoD_og_Verifikation.docx` | Dokumentation og verifikation af DoD |
| `CodeReview.md` | Code Review-gennemgang |
| `Testplan.md` | Testcases og testresultater |
| `Brugertest_3_personer.md` | Dokumentation af brugertests |
| `UI_Heuristik_og_Gestalt.md` | UI-vurdering |
| `Sporbarhed.md` | Sammenhæng mellem krav, kode og tests |
| `Klassediagram.png` | UML-klassediagram |
| `ER_diagram.png` | ER-diagram med Crow's Foot-notation |

Diagrammernes redigerbare Graphviz-kildefiler er også gemt i `Docs/`.

## 9. Kendte afgrænsninger

Projektet er udviklet som en fungerende skoleprototype, ikke som et produktionsklart system.

- En aktivitet har én ansvarlig leder. Flere samtidige ledertilknytninger er ikke implementeret.
- Brugere oprettes gennem demo-seeding, ikke gennem offentlig selvregistrering.
- Der er ikke udviklet et administrationssystem til oprettelse af nye brugere.
- Automatisk test dækker ikke alle mulige brugerforløb og databaseintegrationer.
- Uafhængig Code Review-godkendelse udestår.

## 10. Aflevering

Projektet afleveres i Wiseflow i henhold til skolens anvisninger.

Afleveringsmaterialet bør indeholde:

- Kildekode til Razor Pages-applikationen.
- Testprojektet.
- `Data/schema.sql`.
- Dokumentationen i `Docs/`.
- Denne README.

Ved ZIP-pakning udelades lokale og genererede filer såsom `.vs/`, `bin/`, `obj/` og private konfigurationsfiler med adgangskoder.

Projektets Git-historik og kildekode findes også på GitHub:

https://github.com/martinsir/DeLyseroedeSpejdere