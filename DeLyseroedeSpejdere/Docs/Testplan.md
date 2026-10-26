# Testplan – De Lyserøde Spejdere

**Projekt:** De Lyserøde Spejdere  
**Testdato:** 26. oktober 2026  
**Testmiljø:** Visual Studio 2026, ASP.NET Core Razor Pages, .NET 10, MySQL og Firefox.

## 1. Formål

Formålet med testplanen er at kontrollere, at applikationens funktioner opfylder projektets krav og acceptkriterier.

Der anvendes både manuelle funktionelle tests og automatiske unit tests med xUnit.

## 2. Funktionelle tests

| ID | Testhandling | Forventet resultat | Resultat |
|---|---|---|---|
| T01 | Start applikationen med MySQL og demodata | Aktivitetsoversigt vises | Bestået |
| T02 | Log ind som leder med forkert adgangskode | Login afvises | Ikke testet |
| T03 | Log ind som leder med korrekt adgangskode | Lederpanelet vises | Bestået |
| T04 | Opret en aktivitet | Aktiviteten gemmes og vises | Bestået i lederpanelet |
| T05 | Opret aktivitet med sluttid før starttid | Valideringsfejl vises | Ikke testet |
| T06 | Log ind som spejder og tilmeld aktivitet | Tilmeldingen gemmes | Bestået |
| T07 | Tilmeld samme aktivitet to gange | Anden tilmelding afvises | Ikke testet |
| T08 | Log ind som junior og vælg ridning | Afvises på grund af alderskrav | Ikke testet |
| T09 | Log ind som junior og vælg førstehjælpsaktivitet | Afvises på grund af manglende færdighedstegn | Ikke testet |
| T10 | Se Mine tilmeldinger | Egne tilmeldinger vises | Grundfunktion bestået; adskillelse mellem brugere ikke testet |
| T11 | Frameld aktivitet | Tilmelding fjernes, og plads frigives | Bestået |
| T12 | Åbn /Leader/Create som spejder | Adgang nægtes | Ikke testet på denne side; /Leader blev afvist korrekt |
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

## 4. Unit Tests

Unit tests er implementeret med xUnit og kørt i Visual Studios Test Explorer.

| Testkategori | Antal | Resultat |
|---|---:|---|
| ActivityValidationTests | 5 | Bestået |
| RegistrationRulesTests | 4 | Bestået |
| **I alt** | **9** | **9 bestået, 0 fejlet** |

## 5. Registrerede fejl og rettelser

### CR-01 – Valgfrit færdighedstegn

**Problem:** Feltet RequiredBadge blev behandlet som obligatorisk, selvom feltet skulle være valgfrit.

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

## 6. Supplerende DoD-kontroller

| Kontrol | Status |
|---|---|
| Unit tests | 9/9 bestået |
| Repository Pattern og interfaces | Implementeret |
| UI-heuristikker og Gestaltlove | Dokumenteret; endelig vurdering mangler |
| Code Review | Individuel gennemgang dokumenteret; uafhængig godkendelse mangler |
| Brugertest med tre personer | Intern funktionstest dokumenteret; tre forskellige testpersoner mangler |
| Versionsstyring | Git og GitHub etableret |
| Klassediagram | Udarbejdet; endelig sammenligning mangler |

## 7. Samlet testvurdering

De centrale funktioner til aktivitetsoversigt, login, tilmelding, framelding og administration af aktiviteter er manuelt afprøvet.

De automatiske unit tests er gennemført med 9 beståede tests og ingen fejl.

Under testningen blev to konkrete problemer identificeret og rettet. Begge rettelser er efterfølgende afprøvet.

Der udestår stadig test af ugyldigt login, dobbelt tilmelding, aldersgrænser, færdighedskrav og visse validerings- og adgangsscenarier.

Den samlede teststatus er derfor **delvist gennemført**, og den fulde Definition of Done er endnu ikke opfyldt.