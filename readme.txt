Do projektu byl přidán automatický import dat po spuštění (soubory MeetingCentres.xml, MeetingRooms.xml, reservations.xml). Import je obsažen v třídě DataStore. Do stejných souborů se nyní provádí i export při zavření aplikace (v případě provedení změny). Původní funkčnosti jsou stále obsaženy, takže je v tom trochu zmatek...
Funkcionality týkající se 2. bodu zadání jsou ve složce reservations. S tím že některé funkčnosti jsou v třídě DataStore, která slouží jako univerzální skladiště pro upravování dat.
Do reservations se dostane pomocí tlačítka na horním menu. Pro export dat do jsonu je použita třída ReservationJson.
Rezervace mají vlastní třídu reservations. Je tedy možné zobrazovat, vytvářet, upravovat a mazat jednotlivé rezervace.
