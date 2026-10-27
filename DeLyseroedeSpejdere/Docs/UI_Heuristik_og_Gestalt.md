# Brugervenlighed – evaluering af Nielsens heuristikker, Gestalt og visuelt hierarki

**Projekt:** De Lyserøde Spejdere  
**Dato:** 26. oktober 2026  
**Metode:** Intern ekspertgennemgang af Razor Pages, CSS og skærmbilleder fra de dokumenterede manuelle tests.  
**Omfang:** Aktivitetsoversigt, login, tilmeldinger, lederpanel, aktivitetsformularer og adgangsfejl.

## 1. Formål og afgrænsning

At vurdere brugergrænsefladen i forhold til Jakob Nielsens ti heuristikker, relevante Gestaltlove og visuelt informationshierarki. Gennemgangen bygger på den eksisterende applikation og observerede tests, herunder CR-01 og CR-02. Det er en intern faglig vurdering, ikke en måling med tre eksterne testpersoner. Responsivt layout, fuld tastaturnavigation og farvekontrast er ikke systematisk testet.

## 2. Nielsens ti heuristikker – gennemført gennemgang

| Nr. | Heuristik | Konkret fund i projektet | Vurdering |
|---|---|---|---|
| 1 | Systemets status skal være synlig | Success- og fejlbeskeder efter oprettelse, tilmelding og adgangsafvisning vises via `TempData` i `_Layout.cshtml` (`role="status"` og `role="alert"`). Efter CR-02 vises "Allerede tilmeldt" på aktivitetskortet. | Opfyldt for gennemgåede forløb |
| 2 | Sammenhæng mellem system og virkelighed | Menupunkterne "Aktiviteter", "Mine tilmeldinger" og "Lederpanel" samt ord som alder, pladser og færdighedstegn er naturlige for målgruppen. | Opfyldt |
| 3 | Brugerkontrol og frihed | Spejderen kan framelde sig; lederen kan annullere oprettelse/redigering. Der vises bekræftelse før sletning og framelding. | Opfyldt |
| 4 | Konsistens og standarder | Samme navigation, kort, overskrifter og knapper går igen. Rød bruges til destruktive handlinger, mens almindelige handlinger er blågrønne. | Opfyldt; små tekstvariationer kan ensrettes |
| 5 | Forebyggelse af fejl | Formularvalidering og serverbaserede adgangskrav forhindrer flere ugyldige handlinger; MySQL afviser dubletter (T07). Invalide alders-/mærkekrav afvises på serveren, men tilmeldingsknappen vises stadig på kortet. | Delvist opfyldt – forbedringspunkt |
| 6 | Genkendelse frem for hukommelse | Tidspunkter, ledige pladser, minimumsalder og eventuelle mærkekrav står direkte på aktivitetskortet. | Opfyldt |
| 7 | Fleksibilitet og effektivitet | Rollebestemt menu og lederpanel samler handlinger ét sted. Ingen søgning, filtrering eller sortering af aktiviteter. | Delvist opfyldt – forbedringsmulighed |
| 8 | Æstetisk og minimalistisk design | Oversigten viser få, tydelige kort med titel, beskrivelse, krav og vigtigste handling. Overflødige elementer er begrænset. | Opfyldt for gennemgået desktopvisning |
| 9 | Hjælp til at forstå og rette fejl | Der er afprøvet konkrete meddelelser om forkert login (T02), sluttid før starttid (T05), alder (T08), manglende tegn (T09) og ingen adgang (T12). CR-01 om valgfrit tegn blev rettet. | Opfyldt for afprøvede scenarier |
| 10 | Hjælp og dokumentation | README beskriver opstart og demo-konti, men selve UI'et har ingen samlet Hjælp-side eller FAQ. | Delvist opfyldt – forbedringsmulighed |

**Konklusion på heuristikker:** Alle ti er vurderet, og der findes konkret UI-understøttelse for de fleste. Især forebyggelse af fejl, effektiv navigation og hjælp i applikationen kan forbedres. Resultatet er en **gennemført intern heuristisk evaluering**, ikke et bevis for fejlfri brugervenlighed.

## 3. Gestaltlovene – gennemført gennemgang

| Princip | Observation i applikationen | Vurdering |
|---|---|---|
| Nærhed | Aktivitetens titel, beskrivelse, tidspunkt, antal pladser, krav og knap ligger samlet. Formularlabels ligger tæt ved deres felter. | Anvendt |
| Lighed | Aktivitetskort har samme ramme, baggrund, afstande og typografi. Handlingsknapper har gennemgående farve og form. | Anvendt |
| Fælles region | Hvide kort med kant og diskret skygge opdeler aktiviteter og formularer i tydelige grupper. | Anvendt |
| Figur–baggrund | Hvide kort og mørk tekst fremstår tydeligt på den lysegrå sidebaggrund; den mørke topmenu adskilles visuelt. | Anvendt |
| Kontinuitet | Topnavigation og placering af sidetitler er ens på tværs af oversigt, login og lederpanel. | Anvendt |

**Konklusion på Gestalt:** De fem relevante principper kan identificeres direkte i layoutet og CSS. Det giver en konsekvent og overskuelig struktur i de gennemgåede visninger.

## 4. Visuelt hierarki og læserækkefølge

Det visuelle hierarki bygger på:

1. **Global navigation** øverst med projektets navn og relevante menupunkter.
2. **Sidetitel (`h1`)** med større skrift (omtrent 2 rem på desktop).
3. **Aktivitetstitler (`h2`)** som underordnede overskrifter (omtrent 1,35 rem).
4. **Nøgleinformation** fremhævet med fed skrift (Start, Slut, Leder), og ledige pladser vises i en lille markeret etiket.
5. **Primær handling** i en tydelig blågrøn knap; sletning/framelding er rød.
6. **Feedback** placeres over sidens indhold, så brugeren ser resultatet af en handling.

**Vurdering:** Hierarkiet er logisk, ensartet og let at skimme på de tilgængelige desktopscreenshots. Den konkrete mobilvisning, skærmlæsere og kontrastmålinger er ikke undersøgt særskilt.

## 5. Fund, rettelser og anbefalinger

| ID | Observation eller forbedring | Status |
|---|---|---|
| CR-01 | "Påkrævet færdighedstegn (valgfrit)" blev fejlagtigt krævet ved oprettelse. Rettet ved `string?`; oprettelse af Orienteringsløb blev efterfølgende testet. | Rettet og testet |
| CR-02 | "Tilmeld mig" stod stadig på allerede tilmeldte aktiviteter. Rettet ved at hente egne tilmeldinger med `GetMineAsync()` og vise "Allerede tilmeldt". | Rettet og testet |
| UX-01 | Tilmeld-knappen kan stadig forsøges brugt, selv når spejderen ikke opfylder alders- eller mærkekrav. Serveren afviser korrekt, men UI kunne forklare dette før klik. | Forbedringsforslag |
| UX-02 | Hjælp/FAQ og aktivitetssøgning findes ikke i det gennemgåede UI. | Mulig senere udvidelse |
| UX-03 | Test af responsivt layout, kontrast og fuld tastaturnavigation er ikke dokumenteret. | Mangler særskilt verificering |

## 6. Samlet konklusion og DoD-status

**Heuristikker:** Gennemgået og dokumenteret for den eksisterende applikation.  
**Gestaltlove:** Gennemgået og dokumenteret med konkrete layoutobservationer.  
**Visuelt hierarki:** Gennemgået og vurderet ud fra dokumenterede desktopvisninger.  
**Tre-personers brugertest:** Ikke gennemført med tre forskellige personer. Denne gennemgang kan ikke erstatte kravet.

Vurderingen bør kobles til `DoD_Checkliste.md`, `DoD_og_Verifikation.docx` og de faktiske testresultater i `Testplan.md`.
