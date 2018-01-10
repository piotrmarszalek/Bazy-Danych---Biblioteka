-- tabela Autor
INSERT INTO dbo."Autor" ("Imie", "Nazwisko")  VALUES ('Henryk', 'Sienkiewicz');

-- tabela Gatunek
INSERT INTO dbo."Gatunek" ("Nazwa") VALUES ('Fantasy');

-- tabela Wydawnictwo
INSERT INTO dbo."Wydawnictwo" ("Nazwa") VALUES ('Helion');

-- tabela Miasto
INSERT INTO dbo."Miasto" ("Nazwa") VALUES ('Kraków');

-- tabela Czytelnik
INSERT INTO dbo."Czytelnik" ("Imie", "Nazwisko", "MiastoId", "Login") VALUES ('Jan', 'Kowalski', 1, 'janKowalski');

-- tabela Ksiazka
INSERT INTO dbo."Ksiazka" ("Nazwa", "WydawnictwoId", "GatunekId") VALUES ('Krzyżacy', 1, 1);

-- tabela KsiazkaAutors
INSERT INTO dbo."KsiazkaAutors" ("Ksiazka_KsiazkaId", "Autor_AutorId") VALUES (1, 1);     

-- tabela Magazyn
INSERT INTO dbo."Magazyn" ("DostepnaIlosc", "KsiazkaId") VALUES (5, 1);

-- tabela Transakcja
INSERT INTO dbo."Transakcja" ("DataWypozyczenia", "DataOddania", "CzytelnikId", "KsiazkaId") VALUES ('2001-09-28 01:00:00', '2002-09-28 01:00:00', 1, 1);
