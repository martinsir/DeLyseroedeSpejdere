# Testplan – De Lyserøde Spejdere

**Projekt:** De Lyserøde Spejdere  
**Testdato:** 26. oktober 2026  
**Testmiljø:** Visual Studio 2026, ASP.NET Core Razor Pages, .NET 10, MySQL og Firefox.

## 1. Formål

Formålet med testplanen er at kontrollere, at applikationens funktioner opfylder projektets krav og acceptkriterier.

Der anvendes både manuelle funktionelle tests, databasekontroller og automatiske unit tests med xUnit.

Testresultaterne dokumenteres gennem skærmbilleder, MySQL-forespørgsler og Visual Studios Test Explorer.

## 2. Funktionelle tests

| ID | Testhandling | Forventet resultat | Resultat |
|---|---|---|---|
| T01 | Start applikationen med MySQL og demodata | Aktivitetsoversigt vises | Bestået |
| T02 | Log ind som leder med forkert adgangskode | Login afvises | **Bestået** – fejlmeddelelse vises |
| T03 | Log ind som leder med korrekt adgangskode | Lederpanelet vises | Bestået |
| T04 | Opret en aktivitet | Aktiviteten gemmes og vises | Bestået |
| T05 | Opret aktivitet med sluttid før starttid | Valideringsfejl vises | **Bestået** – ugyldigt tidspunkt afvises |
| T06 | Log ind som spejder og tilmeld aktivitet | Tilmeldingen gemmes | Bestået |
| T07 | Tilmeld samme aktivitet to gange | Anden tilmelding afvises | **Delvist bestået** – MySQL afviser dubletter med fejl 1062; applikationskontrol mangler |
| T08 | Log ind som junior og vælg ridning | Afvises på grund af alderskrav | **Bestået** – minimumsalder håndhæves |
| T09 | Log ind som junior og vælg førstehjælpsaktivitet | Afvises på grund af manglende færdighedstegn | **Bestået** – testet efter midlertidig aldersændring |
| T10 | Se Mine tilmeldinger | Egne tilmeldinger vises | Grundfunktion bestået; adskillelse mellem brugere ikke testet |
| T11 | Frameld aktivitet | Tilmelding fjernes, og plads frigives | Bestået |
| T12 | Åbn /Leader/Create som spejder | Adgang nægtes | **Bestået** – adgang afvist |
| T13 | Se deltagerliste som leder | Navne og alder vises | Bestået |
| T14 | Rediger aktivitet og slet den igen | Data opdateres og slettes | Bestået i brugergrænsefladen |

## 3. Supplerende tests

| ID | Test | Resultat |
|---|---|---|
| ST01 | Login som spejder | Bestået |
| ST02 | Visning af tre tilmeldte aktiviteter | Bestået |
| ST03 | Framelding fra Mad over bål | Bestået |
| ST04 | Kontrol af frigivet plads (8/8) | Bestået |
| ST05 | Oprettelse af Orienteringsløb efter valideringsrettelse | Bestået |
| ST06 | Ændring af maksimalt antal deltagere fra 10 til 12 | Bestået |
| ST07 | Visning af Spejder Emil på deltagerlisten | Bestået |
| ST08 | Sletning af Orienteringsløb | Bestået i lederpanelet |
| ST09 | Direkte adgang til /Leader som spejder | Afvist korrekt |
| ST10 | Visning af "Allerede tilmeldt" efter tilmelding | Bestået |
| ST11 | Forkert adgangskode ved login | Afvist korrekt |
| ST12 | Sluttid før starttid | Valideringsfejl vist |
| ST13 | Dobbelt registrering i MySQL | Afvist med fejl 1062 |
| ST14 | Alder under minimumsgrænse | Afvist korrekt |
| ST15 | Manglende færdighedstegn | Afvist korrekt |
| ST16 | Direkte adgang til /Leader/Create som spejder | Afvist korrekt |

## 4. Unit Tests

Unit tests er implementeret med xUnit og kørt i Visual Studios Test Explorer.

| Testkategori | Antal | Resultat |
|---|---:|---|
| ActivityValidationTests | 5 | Bestået |
| RegistrationRulesTests | 4 | Bestået |
| **I alt** | **9** | **9 bestået, 0 fejlet** |

Testene kontrollerer blandt andet validering af aktiviteternes data samt regler for alder og færdighedstegn.

## 5. Registrerede fejl og rettelser

### CR-01 – Valgfrit færdighedstegn

**Problem:** Feltet `RequiredBadge` blev behandlet som obligatorisk, selvom det skulle være valgfrit.

**Årsag:** Egenskaben var deklareret som en ikke-nullable string.

**Rettelse:** Egenskaben blev ændret fra `string` til `string?`.

**Resultat:** Aktiviteten Orienteringsløb kunne efterfølgende oprettes uden færdighedstegn.

**Status:** Rettet og testet.

### CR-02 – Forkert tilmeldingsstatus

**Problem:** Knappen "Tilmeld mig" blev fortsat vist, selvom spejderen allerede var tilmeldt aktiviteten.

**Rettelse:** `Pages/Activities/Index.cshtml.cs` henter spejderens eksisterende tilmeldinger med `GetMineAsync()`.

Razor-siden `Index.cshtml` kontrollerer de tilmeldte aktivitets-ID'er og viser "Allerede tilmeldt" i stedet for knappen.

**Resultat:** Den opdaterede visning fungerer efter tilmelding.

**Status:** Rettet og testet.

## 6. Kontrol af alders- og færdighedsbegrænsninger

### T08 – Aldersbegrænsning

Testbrugeren Spejder Sofie var registreret med en alder på 10 år.

Hun forsøgte at tilmelde sig Ridning på gården, som kræver en minimumsalder på 12 år.

Systemet viste fejlmeddelelsen:

"Du skal være mindst 12 år."

Tilmeldingen blev afvist, og antallet af ledige pladser forblev uændret.

**Resultat: Bestået.**

### T09 – Færdighedstegn

For at kontrollere færdighedstegnet uafhængigt af alderskravet blev Sofies alder midlertidigt ændret fra 10 til 11 år i MySQL.

Hun forsøgte derefter at tilmelde sig Førstehjælp i naturen, som kræver færdighedstegnet Førstehjælp.

Systemet viste fejlmeddelelsen:

"Aktiviteten kræver færdighedstegnet: Førstehjælp."

Tilmeldingen blev afvist, og antallet af ledige pladser forblev 12 af 12.

Efter testen blev Sofies alder ændret tilbage til 10 år.

**Resultat: Bestået.**

## 7. Databasekontrol – Dobbelt tilmelding

### T07 – Kontrol af unik registrering

Testen blev udført direkte i MySQL Workbench.

Først blev det kontrolleret, at Spejder Emil kun havde én tilmelding til Mad over bål.

Resultatet viste:

**AntalTilmeldinger: 1**

Derefter blev der forsøgt indsat en identisk tilmelding i tabellen `Registrations`.

MySQL returnerede:

`Error 1062: Duplicate entry '2-1' for key 'registrations.PRIMARY'`

Det bekræfter, at databasens sammensatte primærnøgle forhindrer dobbeltregistrering.

**Resultat:** Databasens beskyttelse er bestået.

**Resterende kontrol:** Applikationens håndtering af gentagne tilmeldingsforsøg er ikke særskilt testet.

## 8. Adgangskontrol

### T12 – Spejder forsøger adgang til lederfunktion

Spejder Sofie forsøgte at åbne følgende adresse direkte:

`https://localhost:7215/Leader/Create`

Applikationen omdirigerede brugeren til siden "Ingen adgang".

Fejlmeddelelsen var:

"Din brugerrolle har ikke adgang til denne side."

Det dokumenterer, at den afprøvede lederside er beskyttet af serverbaseret rolleautorisation.

**Resultat: Bestået.**

## 9. Supplerende DoD-kontroller

| Kontrol | Status |
|---|---|
| Unit tests | 9/9 bestået |
| Repository Pattern og interfaces | Implementeret |
| UI-heuristikker og Gestaltlove | Dokumenteret; endelig vurdering mangler |
| Code Review | Individuel teknisk gennemgang dokumenteret; uafhængig godkendelse mangler |
| Brugertest med tre personer | Intern funktionstest dokumenteret; tre forskellige testpersoner mangler |
| Versionsstyring | Git og GitHub etableret |
| Klassediagram | Udarbejdet; endelig sammenligning mangler |

## 10. Samlet testvurdering

De centrale funktioner til aktivitetsoversigt, login, tilmelding, framelding og administration af aktiviteter er manuelt afprøvet.

Derudover er der gennemført negative testscenarier, hvor systemet skal afvise ugyldige handlinger.

Følgende er bekræftet:

- Forkert adgangskode bliver afvist.
- Ugyldigt sluttidspunkt udløser valideringsfejl.
- Databasen forhindrer dobbeltregistrering.
- Aldersbegrænsninger håndhæves.
- Færdighedskrav kontrolleres.
- Spejdere afvises fra beskyttede ledersider.

Alle ni automatiske unit tests er bestået.

To konkrete fejl blev identificeret under de manuelle tests og efterfølgende rettet og afprøvet.

**Samlet status:** De planlagte funktionelle tests er overvejende gennemført. Test af gentagen tilmelding gennem applikationen og enkelte supplerende kvalitetskontroller udestår.

Den fulde Definition of Done er endnu ikke opfyldt, da blandt andet uafhængig Code Review-godkendelse og brugertest med tre forskellige personer mangler.