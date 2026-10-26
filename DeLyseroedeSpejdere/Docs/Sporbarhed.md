# Sporbarhed og versionsstyring – De Lyserøde Spejdere

**Projektperiode:** 19.–30. oktober 2026  
**Repository:** https://github.com/martinsir/DeLyseroedeSpejdere  
**Branch:** main

## 1. Formål

Formålet med sporbarhed er at dokumentere sammenhængen mellem projektets user stories, implementerede funktioner, testresultater og versionsstyring.

Projektet anvender Git og GitHub til versionsstyring af kildekode og dokumentation.

## 2. Sporbarhed mellem User Stories og kode

| User Story | Relevante filer | Test og status |
|---|---|---|
| US-01 – Aktivitetsoversigt | Pages/Activities/Index.cshtml og ActivityRepository.cs | TC01 – Bestået |
| US-02 – Login og roller | Pages/Account, Program.cs og UserRepository.cs | TC02 og TC10 – Grundfunktion testet |
| US-03 – Opret aktivitet | Pages/Leader/Create og ActivityInput.cs | TC06 – Bestået efter rettelse |
| US-04 – Tilmelding | RegistrationRepository.cs og RegistrationRules.cs | TC03 – Grundfunktion bestået |
| US-05 – Mine tilmeldinger | Pages/Scout/Mine | TC04 – Bestået |
| US-06 – Framelding | Pages/Scout/Mine og RegistrationRepository.cs | TC05 – Bestået |
| US-07 – Rediger og slet aktivitet | Pages/Leader/Edit og Pages/Leader/Index | TC07 og TC09 – Bestået |
| US-08 – Deltagerliste | Pages/Leader/Participants | TC08 – Bestået |
| US-09 – Flere ledere pr. aktivitet | Ikke implementeret | Ikke testet |

De implementerede funktioner er manuelt afprøvet. Endelig Definition of Done afhænger desuden af de øvrige kvalitetskrav.

## 3. Registrerede fejl og rettelser

**CR-01 – Valgfrit færdighedstegn**

Problem: Feltet RequiredBadge blev behandlet som obligatorisk.

Rettelse: Egenskaben blev ændret til nullable string (`string?`).

Resultat: Aktiviteten kunne efterfølgende oprettes uden færdighedstegn.

**Status:** Rettet og testet.

**CR-02 – Visning af eksisterende tilmelding**

Problem: Knappen "Tilmeld mig" blev vist, selvom spejderen allerede var tilmeldt.

Rettelse: Aktivitetsoversigten henter eksisterende tilmeldinger med `GetMineAsync()` og viser "Allerede tilmeldt".

Resultat: Den opdaterede visning blev testet og fungerer.

**Status:** Rettet og testet.

## 4. Git og versionsstyring

Projektet er oprettet som et separat Git-repository og uploadet til GitHub.

**Første commit:**

- Commit-ID: `97bb892`
- Beskrivelse: Initial commit - De Lyseroede Spejdere
- Antal filer: 64

Efterfølgende kode- og dokumentationsændringer registreres i Git-historikken på branchen main.

Den lokale databasekonfiguration `appsettings.Development.json` er udelukket fra versionsstyring gennem `.gitignore`.

## 5. Automatiske tests

Projektet indeholder xUnit-tests for aktivitetsvalidering og tilmeldingsregler.

**Resultat fra Visual Studio Test Explorer:**

- 9 tests gennemført
- 9 bestået
- 0 fejlet

De automatiske tests supplerer projektets manuelle funktionelle tests.

## 6. Samlet vurdering

Projektets centrale funktioner er implementeret, og flere user stories er dokumenteret gennem funktionelle tests.

GitHub indeholder kildekode, databasemodel, dokumentation og automatiske tests.

Sporbarheden forbinder projektets krav med implementeringen og de registrerede testresultater.

Der udestår fortsat enkelte test- og godkendelseskrav i projektets Definition of Done.