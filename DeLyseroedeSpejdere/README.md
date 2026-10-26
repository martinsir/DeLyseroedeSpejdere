# De Lyserøde Spejdere – Razor Pages miniprojekt

ASP.NET Core 10 Razor Pages, MySQL og ADO.NET (`MySqlConnector`). Projektet er udviklet til skolecasen om en spejderlejr i Hedeland ved Roskilde.

## Start projektet i Visual Studio

1. Installer/brug **.NET 10 SDK** og en kørende **MySQL Server**. Visual Studio skal understøtte .NET 10 og ASP.NET/webudvikling.
2. Åbn `Data/schema.sql` i **MySQL Workbench** og kør hele scriptet én gang. Databasen `LyseroedeSpejdere` oprettes.
3. Åbn **`DeLyseroedeSpejdere.sln`** i Visual Studio (indeholder både app og xUnit-testprojekt). Vent på, at NuGet-pakken `MySqlConnector` (2.6.2) indlæses.
4. Kopiér `appsettings.json` til `appsettings.Development.json` og ret `Password=CHANGE_ME` i connection string til din **egen MySQL-adgangskode**. Den lokale fil er ignoreret af Git. Alternativt kan du bruge ASP.NET User Secrets.
5. Start projektet med **F5** eller `dotnet run`. Start med **HTTPS**-profilen, hvis Visual Studio spørger. På første start oprettes tre demo-brugere og tre demo-aktiviteter automatisk.
6. Åbn adressen, som Visual Studio viser (som regel `https://localhost:...`).

**Vigtigt:** Projektet kan ikke starte, før MySQL er sat op, og `schema.sql` er kørt. Der er ikke indsat en rigtig adgangskode i de udleverede filer.

### Testbrugere (udelukkende til lokal demonstration)

| Rolle | Brugernavn | Adgangskode |
|---|---|---|
| Leder | `leder` | `Lejr2026!` |
| Spejder, 13 år, med tegn | `spejder` | `Lejr2026!` |
| Spejder, 10 år, uden tegn | `junior` | `Lejr2026!` |

Demo-brugere oprettes med **hash af passwords** og kun, når `DemoUsers:Enabled` er `true`. Slå dette fra og erstat demo-login før eventuel rigtig drift.

## Funktioner

- **Anonym bruger:** se aktiviteter og login.
- **Leder:** opret, rediger og slet aktiviteter; se tilmeldte spejdere.
- **Spejder:** se aktiviteter, tilmeld sig, se egne tilmeldinger og frameld sig.
- **Regler:** minimumsalder, færdighedstegn, maksimalt antal deltagere og ingen dobbelt tilmelding.
- **Adgangskontrol:** ASP.NET Core-cookie authentication, claims og serverbeskyttede sider.
- **Database:** tre tabeller (`Users`, `Activities`, `Registrations`) med relationer og SQL-parametre.

## Arkitektur og pensum

1. `Models/` indeholder C#-klasser og validering.
2. `Repositories/` indeholder SQL-operationer i ADO.NET via `MySqlConnector`.
3. `Pages/` indeholder `.cshtml` (visning) og `.cshtml.cs` (PageModels).
4. `Services/DemoDataSeeder.cs` opretter kun eksempeldatasæt til lokal afprøvning.
5. `Program.cs` registrerer services, cookie authentication og rollebaseret autorisation.

**Database-transaktion:** `RegistrationRepository.JoinAsync` låser aktivitetsrækken (`FOR UPDATE`) før kontrol af antal pladser, så samtidige tilmeldinger ikke overbooker. Tilmeldinger har desuden sammensat primærnøgle `(UserId, ActivityId)`.

**Cookies vs. sessions:** Login benytter en signeret autentifikations-cookie, hvor brugerens rolle ligger i claims. Sessions benyttes ikke til login; det er ikke nødvendigt for de implementerede user stories.

## Dokumentation

Se `Docs/` og den separate Word-fil med user stories og SCRUM-plan i afleveringspakken. SCRUM-perioden følger opgaveperioden 19.–30. oktober. Planlægning og simulerede møder skal ikke forveksles med dokumentation for faktisk udført arbejde.

## Kontrol før Wiseflow

- [ ] Kør SQL-scriptet og start webappen uden fejl.
- [ ] Log ind med både `leder` og `spejder`.
- [ ] Opret en aktivitet og kontroller, at den vises.
- [ ] Test tilmelding, dobbelt tilmelding, alderskrav, tegnkrav og framelding.
- [ ] Kontrollér at `spejder` ikke kan åbne `/Leader/Create`.
- [ ] Opdater faktisk sprintstatus, testresultater og gruppemedlemmer i dokumentet.
- [ ] ZIP kildekode og SQL + dokumentation. Udelad `.vs`, `bin`, `obj` og private credentials.

## Afgrænsning

Casen beskriver aktiviteter med **en til flere ledere**. Denne prototype knytter én ansvarlig leder til en aktivitet. Flere samtidige ledertilknytninger er med i Product Backlog, men er ikke implementeret. Brugeroprettelse foretages kun gennem seeding til en kontrolleret skoledemo; der er ikke selvregistrering.

## Supplerende Definition of Done (underviserens DoD.docx)

Dokumentationen `Docs/DoD_og_Verifikation.docx` og `Docs/DoD_Checkliste.md` gennemgår alle 9 krav fra den uploadede DoD. Tilføjet i denne opdatering:

- **Passende interfaces:** `IActivityRepository`, `IRegistrationRepository`, `IUserRepository`, registreret i dependency injection.
- **Unit tests:** xUnit-projekt i `Tests/DeLyseroedeSpejdere.Tests`. I Visual Studio: *Test > Run All Tests*; alternativt `dotnet test DeLyseroedeSpejdere.sln`. Resultater må først markeres bestået, når tests faktisk er kørt.
- **Klassediagram:** `Docs/Klassediagram.png`, kildefil `Docs/Klassediagram.dot`.
- **Nielsens heuristikker og Gestalt:** `Docs/UI_Heuristik_og_Gestalt.md`.
- **Code Review:** `Docs/CodeReview.md` kræver gennemgang og godkendelse af en anden person.
- **Sporbarhed:** `Docs/Sporbarhed.md` forbinder user stories, filer og tests, med plads til reelle Git-commits.
- **Tre brugertests:** `Docs/Brugertest_3_personer.md` har tre udfyldningsark til reelle testpersoner.

**OBS:** Projektet er ikke kompileret/testet i dette miljø, da .NET SDK og MySQL-testmiljø ikke er til rådighed. En færdig ZIP erstatter ikke nødvendig kørsel, code review eller faktiske brugertests.
