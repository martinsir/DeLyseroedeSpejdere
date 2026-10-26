# Definition of Done – De Lyserøde Spejdere

**Projektperiode:** 19.–30. oktober 2026  
**Senest opdateret:** 26. oktober 2026

## 1. Formål

Definition of Done (DoD) beskriver de kvalitetskrav, der skal være opfyldt, før en user story kan betragtes som færdig.

Kravene tager udgangspunkt i underviserens DoD og anvendes til at vurdere projektets implementering, dokumentation og testresultater.

## 2. DoD – Krav og status

| Nr. | Krav | Dokumentation | Status |
|---|---|---|---|
| 1 | Repository Pattern | Repositories adskiller databaseoperationer fra Razor Pages | Implementeret og delvist kodegennemgået |
| 2 | Jakob Nielsens relevante heuristikker | UI_Heuristik_og_Gestalt.md og Razor Pages | Dokumenteret, mangler endelig brugervurdering |
| 3 | Gestaltlovene | UI-design, CSS og strukturering af sider | Indarbejdet, mangler endelig vurdering |
| 4 | Passende interfaces | Tre repository-interfaces og Dependency Injection | Implementeret og kompileret |
| 5 | Opdateret klassediagram | Klassediagram.png og Klassediagram.dot | Udarbejdet, mangler endelig sammenligning |
| 6 | Godkendt Code Review | CodeReview.md | Individuel gennemgang dokumenteret, afventer uafhængig godkendelse |
| 7 | Versionsstyring og sporbarhed | Git, GitHub og Sporbarhed.md | Git og GitHub fungerer; sporbarhedsdokumentet skal ajourføres |
| 8 | Brugertest med tre personer | Brugertest_3_personer.md | Manuel test gennemført; tre uafhængige testpersoner mangler |
| 9 | Unit tests | xUnit i Visual Studio Test Explorer | 9 ud af 9 bestået |

## 3. Gennemførte funktionelle tests

Følgende funktioner er afprøvet manuelt:

- TC01 – Vis aktivitetsoversigt: Bestået
- TC02 – Login som spejder: Bestået
- TC03 – Tilmeld aktivitet: Grundfunktion bestået
- TC04 – Vis egne tilmeldinger: Bestået
- TC05 – Framelding og frigivelse af plads: Bestået
- TC06 – Opret aktivitet: Bestået efter fejlrettelse
- TC07 – Rediger aktivitet: Bestået
- TC08 – Vis deltagere: Bestået
- TC09 – Slet aktivitet: Bestået
- TC10 – Adgangskontrol: Bestået for den afprøvede lederside
- TC11 – Unit tests: 9/9 bestået

## 4. Fundne fejl og rettelser

**CR-01 – Validering af færdighedstegn**

Et valgfrit færdighedstegn blev behandlet som obligatorisk ved oprettelse af aktiviteter.

Rettelse: `RequiredBadge` blev ændret til `string?`.

Status: Rettet og testet.

**CR-02 – Tilmeldingsknappens status**

Knappen "Tilmeld mig" blev fortsat vist efter gennemført tilmelding.

Rettelse: Aktivitetsoversigten anvender `GetMineAsync()` til at identificere eksisterende tilmeldinger og viser "Allerede tilmeldt".

Status: Rettet og testet.

## 5. Versionsstyring

Projektet anvender Git og GitHub.

**Repository:** https://github.com/martinsir/DeLyseroedeSpejdere

**Branch:** main

Kildekode, dokumentation, database-script og unit tests er versionsstyret.

Den lokale databasekonfiguration er udelukket via `.gitignore`.

## 6. Samlet vurdering

Projektets centrale funktioner er implementeret og afprøvet.

De automatiske tests er gennemført uden fejl, og de fejl, der blev identificeret under den manuelle test, er rettet.

Definition of Done er dog endnu ikke fuldt opfyldt. Der udestår blandt andet uafhængig Code Review-godkendelse, brugertest med tre forskellige personer og endelig kontrol af dokumentation og diagrammer.

User stories, der fungerer, men ikke har opfyldt samtlige DoD-krav, betragtes som **Implementeret – afventer DoD** frem for endeligt Done.