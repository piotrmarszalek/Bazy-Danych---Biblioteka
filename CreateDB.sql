-- tworzenie schematu

CREATE SCHEMA dbo;


-- tworzenie funkcji

CREATE FUNCTION aktualizuj_magazyn() RETURNS trigger
    LANGUAGE plpgsql
    AS $$ 
            BEGIN INSERT INTO dbo."Magazyn" ("DostepnaIlosc", "KsiazkaId") VALUES (0, NEW."KsiazkaId"); RETURN NEW; 
            END $$;


ALTER FUNCTION public.aktualizuj_magazyn() OWNER TO postgres;

--
-- TOC entry 207 (class 1255 OID 51883)
-- Name: data_wypozyczenia_data_oddania(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION data_wypozyczenia_data_oddania() RETURNS trigger
    LANGUAGE plpgsql
    AS $$ 
            BEGIN IF OLD."DataWypozyczenia" > NEW."DataOddania" THEN RAISE EXCEPTION 'data oddania nie moze byc przed data wypozyczenia'; END IF; RETURN NEW; 
            END $$;


ALTER FUNCTION public.data_wypozyczenia_data_oddania() OWNER TO postgres;

--
-- TOC entry 208 (class 1255 OID 51884)
-- Name: dostepnosc_tytulu(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION dostepnosc_tytulu() RETURNS trigger
    LANGUAGE plpgsql
    AS $$ 
            BEGIN IF liczba_dostepnych_ksiazek(NEW."KsiazkaId") = 0 THEN RAISE EXCEPTION 'brak sztuk w magazynie'; END IF; RETURN NEW; 
            END $$;


ALTER FUNCTION public.dostepnosc_tytulu() OWNER TO postgres;

--
-- TOC entry 202 (class 1255 OID 51878)
-- Name: liczba_dostepnych_ksiazek(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION liczba_dostepnych_ksiazek(id integer) RETURNS integer
    LANGUAGE plpgsql
    AS $_$ DECLARE dostepna_ilosc INTEGER; 
            BEGIN SELECT "DostepnaIlosc" into dostepna_ilosc from dbo."Magazyn" where "KsiazkaId"=$1; RETURN dostepna_ilosc; 
            END; $_$;


ALTER FUNCTION public.liczba_dostepnych_ksiazek(id integer) OWNER TO postgres;

--
-- TOC entry 204 (class 1255 OID 51880)
-- Name: usun_ksiazke(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION usun_ksiazke() RETURNS trigger
    LANGUAGE plpgsql
    AS $$ 
            BEGIN DELETE FROM dbo."Ksiazka" WHERE dbo."Ksiazka"."KsiazkaId" = old."Ksiazka_KsiazkaId"; RETURN null; 
            END $$;


ALTER FUNCTION public.usun_ksiazke() OWNER TO postgres;

--
-- TOC entry 205 (class 1255 OID 51881)
-- Name: usun_ksiazke_z_tabeli_laczacej(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION usun_ksiazke_z_tabeli_laczacej() RETURNS trigger
    LANGUAGE plpgsql
    AS $$ 
            BEGIN DELETE FROM dbo."KsiazkaAutors" WHERE dbo."KsiazkaAutors"."Autor_AutorId" = old."AutorId"; RETURN old; 
            END $$;


ALTER FUNCTION public.usun_ksiazke_z_tabeli_laczacej() OWNER TO postgres;

--
-- TOC entry 206 (class 1255 OID 51882)
-- Name: usun_magazyn(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION usun_magazyn() RETURNS trigger
    LANGUAGE plpgsql
    AS $$ 
            BEGIN DELETE FROM dbo."Magazyn" WHERE "KsiazkaId" = old."KsiazkaId"; RETURN old; 
            END $$;


ALTER FUNCTION public.usun_magazyn() OWNER TO postgres;

--
-- TOC entry 209 (class 1255 OID 51885)
-- Name: zmniejsz_ilosc_dostepnych_sztuk_o_jeden(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION zmniejsz_ilosc_dostepnych_sztuk_o_jeden() RETURNS trigger
    LANGUAGE plpgsql
    AS $$ 
            BEGIN UPDATE dbo."Magazyn" SET "DostepnaIlosc" = "DostepnaIlosc" -1; RETURN NULL; 
            END; $$;


ALTER FUNCTION public.zmniejsz_ilosc_dostepnych_sztuk_o_jeden() OWNER TO postgres;

--
-- TOC entry 210 (class 1255 OID 51886)
-- Name: zwieksz_ilosc_dostepnych_sztuk_o_jeden(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION zwieksz_ilosc_dostepnych_sztuk_o_jeden() RETURNS trigger
    LANGUAGE plpgsql
    AS $$ 
            BEGIN UPDATE dbo."Magazyn" SET "DostepnaIlosc" = "DostepnaIlosc" +1; RETURN NULL; 
            END; $$;






-- tworzenie tabel

-- Table: dbo."Autor"

-- DROP TABLE dbo."Autor";

CREATE TABLE dbo."Autor"
(
  "AutorId" serial NOT NULL,
  "Imie" text NOT NULL DEFAULT ''::text,
  "Nazwisko" text NOT NULL DEFAULT ''::text,
  CONSTRAINT "PK_dbo.Autor" PRIMARY KEY ("AutorId")
)
WITH (
  OIDS=FALSE
);
ALTER TABLE dbo."Autor"
  OWNER TO postgres;

-- Index: dbo."Autor_ImieNazwisko"

-- DROP INDEX dbo."Autor_ImieNazwisko";

CREATE UNIQUE INDEX "Autor_ImieNazwisko"
  ON dbo."Autor"
  USING btree
  ("Imie" COLLATE pg_catalog."default", "Nazwisko" COLLATE pg_catalog."default");


-- Trigger: usun_ksiazke_z_tabeli_laczacej on dbo."Autor"

-- DROP TRIGGER usun_ksiazke_z_tabeli_laczacej ON dbo."Autor";

CREATE TRIGGER usun_ksiazke_z_tabeli_laczacej
  BEFORE DELETE
  ON dbo."Autor"
  FOR EACH ROW
  EXECUTE PROCEDURE public.usun_ksiazke_z_tabeli_laczacej();













-- Table: dbo."Gatunek"

-- DROP TABLE dbo."Gatunek";

CREATE TABLE dbo."Gatunek"
(
  "GatunekId" serial NOT NULL,
  "Nazwa" text NOT NULL DEFAULT ''::text,
  CONSTRAINT "PK_dbo.Gatunek" PRIMARY KEY ("GatunekId")
)
WITH (
  OIDS=FALSE
);
ALTER TABLE dbo."Gatunek"
  OWNER TO postgres;

-- Index: dbo."Gatunek_IX_Nazwa"

-- DROP INDEX dbo."Gatunek_IX_Nazwa";

CREATE UNIQUE INDEX "Gatunek_IX_Nazwa"
  ON dbo."Gatunek"
  USING btree
  ("Nazwa" COLLATE pg_catalog."default");












-- Table: dbo."Wydawnictwo"

-- DROP TABLE dbo."Wydawnictwo";

CREATE TABLE dbo."Wydawnictwo"
(
  "WydawnictwoId" serial NOT NULL,
  "Nazwa" text NOT NULL DEFAULT ''::text,
  CONSTRAINT "PK_dbo.Wydawnictwo" PRIMARY KEY ("WydawnictwoId")
)
WITH (
  OIDS=FALSE
);
ALTER TABLE dbo."Wydawnictwo"
  OWNER TO postgres;

-- Index: dbo."Wydawnictwo_IX_Nazwa"

-- DROP INDEX dbo."Wydawnictwo_IX_Nazwa";

CREATE UNIQUE INDEX "Wydawnictwo_IX_Nazwa"
  ON dbo."Wydawnictwo"
  USING btree
  ("Nazwa" COLLATE pg_catalog."default");













-- Table: dbo."Ksiazka"

-- DROP TABLE dbo."Ksiazka";

CREATE TABLE dbo."Ksiazka"
(
  "KsiazkaId" serial NOT NULL,
  "Nazwa" text NOT NULL DEFAULT ''::text,
  "WydawnictwoId" integer NOT NULL DEFAULT 0,
  "GatunekId" integer NOT NULL DEFAULT 0,
  CONSTRAINT "PK_dbo.Ksiazka" PRIMARY KEY ("KsiazkaId"),
  CONSTRAINT "FK_dbo.Ksiazka_dbo.Gatunek_GatunekId" FOREIGN KEY ("GatunekId")
      REFERENCES dbo."Gatunek" ("GatunekId") MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE CASCADE,
  CONSTRAINT "FK_dbo.Ksiazka_dbo.Wydawnictwo_WydawnictwoId" FOREIGN KEY ("WydawnictwoId")
      REFERENCES dbo."Wydawnictwo" ("WydawnictwoId") MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE CASCADE
)
WITH (
  OIDS=FALSE
);
ALTER TABLE dbo."Ksiazka"
  OWNER TO postgres;

-- Index: dbo."Ksiazka_IX_GatunekId"

-- DROP INDEX dbo."Ksiazka_IX_GatunekId";

CREATE INDEX "Ksiazka_IX_GatunekId"
  ON dbo."Ksiazka"
  USING btree
  ("GatunekId");

-- Index: dbo."Ksiazka_IX_Nazwa"

-- DROP INDEX dbo."Ksiazka_IX_Nazwa";

CREATE UNIQUE INDEX "Ksiazka_IX_Nazwa"
  ON dbo."Ksiazka"
  USING btree
  ("Nazwa" COLLATE pg_catalog."default");

-- Index: dbo."Ksiazka_IX_WydawnictwoId"

-- DROP INDEX dbo."Ksiazka_IX_WydawnictwoId";

CREATE INDEX "Ksiazka_IX_WydawnictwoId"
  ON dbo."Ksiazka"
  USING btree
  ("WydawnictwoId");


-- Trigger: aktualizuj_magazyn on dbo."Ksiazka"

-- DROP TRIGGER aktualizuj_magazyn ON dbo."Ksiazka";

CREATE TRIGGER aktualizuj_magazyn
  AFTER INSERT
  ON dbo."Ksiazka"
  FOR EACH ROW
  EXECUTE PROCEDURE public.aktualizuj_magazyn();

-- Trigger: usun_magazyn on dbo."Ksiazka"

-- DROP TRIGGER usun_magazyn ON dbo."Ksiazka";

CREATE TRIGGER usun_magazyn
  BEFORE DELETE
  ON dbo."Ksiazka"
  FOR EACH ROW
  EXECUTE PROCEDURE public.usun_magazyn();













-- Table: dbo."KsiazkaAutors"

-- DROP TABLE dbo."KsiazkaAutors";

CREATE TABLE dbo."KsiazkaAutors"
(
  "Ksiazka_KsiazkaId" integer NOT NULL DEFAULT 0,
  "Autor_AutorId" integer NOT NULL DEFAULT 0,
  CONSTRAINT "PK_dbo.KsiazkaAutors" PRIMARY KEY ("Ksiazka_KsiazkaId", "Autor_AutorId"),
  CONSTRAINT "FK_dbo.KsiazkaAutors_dbo.Autor_Autor_AutorId" FOREIGN KEY ("Autor_AutorId")
      REFERENCES dbo."Autor" ("AutorId") MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE CASCADE,
  CONSTRAINT "FK_dbo.KsiazkaAutors_dbo.Ksiazka_Ksiazka_KsiazkaId" FOREIGN KEY ("Ksiazka_KsiazkaId")
      REFERENCES dbo."Ksiazka" ("KsiazkaId") MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE CASCADE
)
WITH (
  OIDS=FALSE
);
ALTER TABLE dbo."KsiazkaAutors"
  OWNER TO postgres;

-- Index: dbo."KsiazkaAutors_IX_Autor_AutorId"

-- DROP INDEX dbo."KsiazkaAutors_IX_Autor_AutorId";

CREATE INDEX "KsiazkaAutors_IX_Autor_AutorId"
  ON dbo."KsiazkaAutors"
  USING btree
  ("Autor_AutorId");

-- Index: dbo."KsiazkaAutors_IX_Ksiazka_KsiazkaId"

-- DROP INDEX dbo."KsiazkaAutors_IX_Ksiazka_KsiazkaId";

CREATE INDEX "KsiazkaAutors_IX_Ksiazka_KsiazkaId"
  ON dbo."KsiazkaAutors"
  USING btree
  ("Ksiazka_KsiazkaId");


-- Trigger: usun_ksiazke on dbo."KsiazkaAutors"

-- DROP TRIGGER usun_ksiazke ON dbo."KsiazkaAutors";

CREATE TRIGGER usun_ksiazke
  AFTER DELETE
  ON dbo."KsiazkaAutors"
  FOR EACH ROW
  EXECUTE PROCEDURE public.usun_ksiazke();



















-- Table: dbo."Magazyn"

-- DROP TABLE dbo."Magazyn";

CREATE TABLE dbo."Magazyn"
(
  "KsiazkaId" integer NOT NULL DEFAULT 0,
  "DostepnaIlosc" integer NOT NULL DEFAULT 0,
  CONSTRAINT "PK_dbo.Magazyn" PRIMARY KEY ("KsiazkaId"),
  CONSTRAINT "FK_dbo.Magazyn_dbo.Ksiazka_KsiazkaId" FOREIGN KEY ("KsiazkaId")
      REFERENCES dbo."Ksiazka" ("KsiazkaId") MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
)
WITH (
  OIDS=FALSE
);
ALTER TABLE dbo."Magazyn"
  OWNER TO postgres;

-- Index: dbo."Magazyn_IX_KsiazkaId"

-- DROP INDEX dbo."Magazyn_IX_KsiazkaId";

CREATE INDEX "Magazyn_IX_KsiazkaId"
  ON dbo."Magazyn"
  USING btree
  ("KsiazkaId");












-- Table: dbo."Miasto"

-- DROP TABLE dbo."Miasto";

CREATE TABLE dbo."Miasto"
(
  "MiastoId" serial NOT NULL,
  "Nazwa" text NOT NULL DEFAULT ''::text,
  CONSTRAINT "PK_dbo.Miasto" PRIMARY KEY ("MiastoId")
)
WITH (
  OIDS=FALSE
);
ALTER TABLE dbo."Miasto"
  OWNER TO postgres;

-- Index: dbo."Miasto_IX_Nazwa"

-- DROP INDEX dbo."Miasto_IX_Nazwa";

CREATE UNIQUE INDEX "Miasto_IX_Nazwa"
  ON dbo."Miasto"
  USING btree
  ("Nazwa" COLLATE pg_catalog."default");










-- Table: dbo."Czytelnik"

-- DROP TABLE dbo."Czytelnik";

CREATE TABLE dbo."Czytelnik"
(
  "CzytelnikId" serial NOT NULL,
  "Imie" text NOT NULL DEFAULT ''::text,
  "Nazwisko" text NOT NULL DEFAULT ''::text,
  "MiastoId" integer NOT NULL DEFAULT 0,
  "Login" text NOT NULL DEFAULT ''::text,
  CONSTRAINT "PK_dbo.Czytelnik" PRIMARY KEY ("CzytelnikId"),
  CONSTRAINT "FK_dbo.Czytelnik_dbo.Miasto_MiastoId" FOREIGN KEY ("MiastoId")
      REFERENCES dbo."Miasto" ("MiastoId") MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE CASCADE
)
WITH (
  OIDS=FALSE
);
ALTER TABLE dbo."Czytelnik"
  OWNER TO postgres;

-- Index: dbo."Czytelnik_ImieNazwiskoMiastoLogin"

-- DROP INDEX dbo."Czytelnik_ImieNazwiskoMiastoLogin";

CREATE UNIQUE INDEX "Czytelnik_ImieNazwiskoMiastoLogin"
  ON dbo."Czytelnik"
  USING btree
  ("Imie" COLLATE pg_catalog."default", "Nazwisko" COLLATE pg_catalog."default", "MiastoId", "Login" COLLATE pg_catalog."default");











-- Table: dbo."Transakcja"

-- DROP TABLE dbo."Transakcja";

CREATE TABLE dbo."Transakcja"
(
  "TransakcjaId" serial NOT NULL,
  "CzytelnikId" integer NOT NULL DEFAULT 0,
  "KsiazkaId" integer NOT NULL DEFAULT 0,
  "DataWypozyczenia" timestamp without time zone NOT NULL DEFAULT '-infinity'::timestamp without time zone,
  "DataOddania" timestamp without time zone,
  CONSTRAINT "PK_dbo.Transakcja" PRIMARY KEY ("TransakcjaId"),
  CONSTRAINT "FK_dbo.Transakcja_dbo.Czytelnik_CzytelnikId" FOREIGN KEY ("CzytelnikId")
      REFERENCES dbo."Czytelnik" ("CzytelnikId") MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE CASCADE,
  CONSTRAINT "FK_dbo.Transakcja_dbo.Ksiazka_KsiazkaId" FOREIGN KEY ("KsiazkaId")
      REFERENCES dbo."Ksiazka" ("KsiazkaId") MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE CASCADE
)
WITH (
  OIDS=FALSE
);
ALTER TABLE dbo."Transakcja"
  OWNER TO postgres;

-- Index: dbo."Transakcja_IX_CzytelnikId"

-- DROP INDEX dbo."Transakcja_IX_CzytelnikId";

CREATE INDEX "Transakcja_IX_CzytelnikId"
  ON dbo."Transakcja"
  USING btree
  ("CzytelnikId");

-- Index: dbo."Transakcja_IX_KsiazkaId"

-- DROP INDEX dbo."Transakcja_IX_KsiazkaId";

CREATE INDEX "Transakcja_IX_KsiazkaId"
  ON dbo."Transakcja"
  USING btree
  ("KsiazkaId");


-- Trigger: data_wypozyczenia_data_oddania on dbo."Transakcja"

-- DROP TRIGGER data_wypozyczenia_data_oddania ON dbo."Transakcja";

CREATE TRIGGER data_wypozyczenia_data_oddania
  BEFORE UPDATE
  ON dbo."Transakcja"
  FOR EACH ROW
  EXECUTE PROCEDURE public.data_wypozyczenia_data_oddania();

-- Trigger: dostepnosc_tytulu on dbo."Transakcja"

-- DROP TRIGGER dostepnosc_tytulu ON dbo."Transakcja";

CREATE TRIGGER dostepnosc_tytulu
  BEFORE INSERT
  ON dbo."Transakcja"
  FOR EACH ROW
  EXECUTE PROCEDURE public.dostepnosc_tytulu();

-- Trigger: zmniejsz_ilosc_dostepnych_sztuk_o_jeden on dbo."Transakcja"

-- DROP TRIGGER zmniejsz_ilosc_dostepnych_sztuk_o_jeden ON dbo."Transakcja";

CREATE TRIGGER zmniejsz_ilosc_dostepnych_sztuk_o_jeden
  AFTER INSERT
  ON dbo."Transakcja"
  FOR EACH ROW
  EXECUTE PROCEDURE public.zmniejsz_ilosc_dostepnych_sztuk_o_jeden();

-- Trigger: zwieksz_ilosc_dostepnych_sztuk_o_jeden on dbo."Transakcja"

-- DROP TRIGGER zwieksz_ilosc_dostepnych_sztuk_o_jeden ON dbo."Transakcja";

CREATE TRIGGER zwieksz_ilosc_dostepnych_sztuk_o_jeden
  AFTER UPDATE
  ON dbo."Transakcja"
  FOR EACH ROW
  EXECUTE PROCEDURE public.zwieksz_ilosc_dostepnych_sztuk_o_jeden();
