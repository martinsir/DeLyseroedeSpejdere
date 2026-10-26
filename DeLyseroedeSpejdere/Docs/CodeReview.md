# Code Review – De Lyserøde Spejdere

## 1. Formål

Formålet med Code Review er at kontrollere projektets kodekvalitet, arkitektur, sikkerhed og funktionalitet.

Projektet er udviklet med ASP.NET Core Razor Pages, C#, ADO.NET og MySQL.

Kodegennemgangen tager udgangspunkt i projektets Definition of Done (DoD), user stories og acceptkriterier.

## 2. Oplysninger om gennemgangen

| Oplysning | Beskrivelse |
|---|---|
| Projekt | De Lyserøde Spejdere |
| Dato | 26-10-2026 |
| Gennemgang | Individuel teknisk gennemgang |
| Git-branch | main |
| Git-commit | 97bb892 |
| Repository | https://github.com/martinsir/DeLyseroedeSpejdere |
| Build | 2 projekter kompileret uden fejl |
| Unit Tests | 9 bestået, 0 fejlet |
| Samlet status | Delvist gennemgået – afventer uafhængig godkendelse |

## 3. Kontrolpunkter

| Kontrolpunkt | Resultat | Status |
|---|---|---|
| Repository Pattern | Databaseoperationer er samlet i repository-klasser og adskilt fra Razor Pages. | Implementeret |
| Interfaces og Dependency Injection | Repository-interfaces er oprettet og anvendes med Dependency Injection i Program.cs. | Implementeret |
| SQL Injection | De gennemgåede SQL-forespørgsler anvender parameteriserede kommandoer. | Foreløbigt godkendt |
| Login og roller | Leder- og spejderroller anvender adgangskontrol på serveren. Direkte adgang til lederpanelet som spejder blev afvist. | Testet |
| Password-hashing og cookies | Password-hashing og cookie authentication er implementeret. Login og logout er afprøvet. | Delvist gennemgået |
| Dobbelt tilmelding og overbooking | Regler for tilmelding er implementeret. Grundlæggende tilmelding og framelding er testet. | Mangler yderligere test |
| Inputvalidering | Fejlen med obligatorisk færdighedstegn er rettet ved at gøre egenskaben nullable. | Rettet og testet |
| Unit Tests | 5 ActivityValidationTests og 4 RegistrationRulesTests er gennemført. | 9/9 bestået |
| Kodestil og navngivning | Projektet anvender separate mapper til Models, Repositories, Services og Pages. | Mangler fuld gennemgang |
| Klassediagram | Opdateret klassediagram med repository-interfaces og centrale klasser foreligger. | Endelig sammenligning mangler |
| Versionsstyring | Projektet er registreret med Git og uploadet til GitHub på branchen main. | Gennemført |

## 4. Funktionelle testresultater

Følgende funktioner er afprøvet manuelt i Razor Pages-applikationen.

| Test ID | Funktion | Resultat |
|---|---|---|
| TC01 | Vis aktivitetsoversigt | Bestået |
| TC02 | Login som spejder | Bestået |
| TC03 | Tilmelding til aktiviteter | Grundfunktion bestået |
| TC04 | Vis egne tilmeldinger | Bestået |
| TC05 | Framelding og frigivelse af plads | Bestået |
| TC06 | Leder opretter aktivitet | Bestået |
| TC07 | Leder redigerer aktivitet | Bestået |
| TC08 | Leder viser deltagere | Bestået |
| TC09 | Leder sletter aktivitet | Bestået |
| TC10 | Spejder forsøger adgang til lederpanel | Bestået |
| TC11 | Automatiske unit tests | 9/9 bestået |

Testene dokumenterer, at de centrale funktioner virker i den afprøvede løsning. De dokumenterer ikke alle tænkelige fejlsituationer.

## 5. Fundne fejl og rettelser

### CR-01 – Forkert validering af valgfrit færdighedstegn

**Fil:** `Models/ActivityInput.cs`

**Problem:**
Feltet RequiredBadge var angivet som valgfrit, men ASP.NET Core-valideringen krævede alligevel, at brugeren udfyldte feltet.

**Rettelse:**
Egenskaben blev ændret fra `string` til `string?`, så en tom værdi accepteres.

**Resultat:**
Aktiviteten Orienteringsløb blev efterfølgende oprettet uden et færdighedstegn.

**Status:** Rettet og manuelt testet.

### CR-02 – Tydeligere visning af eksisterende tilmeldinger

**Fil:** `Pages/Activities/Index.cshtml`

**Observation:**
Knappen "Tilmeld mig" vises fortsat på aktiviteter, selv når brugeren allerede er tilmeldt.

**Forslag:**
Vis eksempelvis "Allerede tilmeldt" eller tilbyd en frameldingsknap.

**Rettelse:**
Aktivitetsoversigten er opdateret, så systemet
kontrollerer spejderens eksisterende tilmeldinger
via GetMineAsync().

Knappen "Tilmeld mig" erstattes med
"Allerede tilmeldt", når spejderen er tilmeldt.

**Testresultat:**
Rettelsen blev manuelt testet i Razor Pages.
Visningen fungerer efter tilmelding.

**Status:** Rettet og testet.

### CR-03 – Yderligere test af tilmeldingsbegrænsninger

**Filer:** `RegistrationRepository.cs` og `RegistrationRules.cs`

**Observation:**
De grundlæggende tilmeldinger fungerer. Der mangler dog yderligere verifikation af dobbelt tilmelding, fuldt bookede aktiviteter og samtidige tilmeldinger.

**Status:** Afventer yderligere test.

## 6. Sikkerhed

Følgende er kontrolleret:

- Spejdere bliver afvist ved forsøg på direkte adgang til lederpanelet.
- Applikationen bruger cookie authentication og rollebaseret adgangskontrol.
- Lokal databasekonfiguration opbevares i `appsettings.Development.json`.
- Filen er udelukket fra Git gennem `.gitignore`.
- GitHub-repositoryet indeholder ikke den lokale databasekonfigurationsfil.

Demobrugerne er beregnet til udvikling og demonstration. De må ikke betragtes som produktionskonti.

## 7. Unit Tests

Automatiske tests blev udført i Visual Studios Test Explorer den 26-10-2026.

**Resultat:**
- Antal tests: 9
- Bestået: 9
- Fejlet: 0
- Sprunget over: 0

Testene omfatter validering af aktiviteter og regler for spejdertilmelding.

Alle ni tests blev gennemført uden fejl.

## 8. Samlet vurdering

Projektets centrale funktioner er implementeret og afprøvet gennem manuelle tests og automatiske unit tests.

Projektet anvender Repository Pattern, interfaces, Dependency Injection, ADO.NET, MySQL og ASP.NET Core Razor Pages.

Under testforløbet blev en valideringsfejl identificeret og rettet.

Der er fortsat forbedringsmuligheder inden for brugergrænseflade, håndtering af dobbelt tilmelding og yderligere verifikation af sikkerhed og dataintegritet.

Den individuelle gennemgang bidrager til kvalitetssikringen, men erstatter ikke en uafhængig Code Review-godkendelse.

## 9. Godkendelse

**Gennemgangstype:** Individuel teknisk gennemgang

**Dato:** 26-10-2026

**Commit gennemgået:** 97bb892

**Resultat:** Foreløbig teknisk gennemgang gennemført.

**Uafhængig reviewer:** Ikke tilknyttet

**Endelig Code Review-godkendelse:** Afventer

**Definition of Done:** Kravet om uafhængig Code Review-godkendelse er endnu ikke opfyldt.