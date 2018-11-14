Erstellt Files mit einem Standart Layout.

@SID = Seasone Identification Number.
@FilePath = Der Path des Ordners wo die Datein gespeichert werden.
@CheckID() = Gibt an ob die @SID schon vorhanden ist.
@AddID() = Fügt die @SID zur ArrayList hinzu.
@RemoveID() = Löscht die @SID von der ArrayList.
@CreatFile() = Erstellt eine @SID [CreatRandomID()] und erzeugt eine Datei mit dem Standart Layout.
@SetFile() = Erstellt das Standart Layout.
@SetValue() = Setzt einen Wert in der Angegebenen @SID Datei
@GetValue() = Gibt an hand der @SID Datei den Gesuchten Wert zurück.
@RemoveFile() = Löscht die Angegebenen @SID Datei.

SetFile() = Hier kann das Standart Layout verändert werden.
                fileWriter.WriteLine("InGameName="); <-- Setzt einen Key ohne Value.
                fileWriter.WriteLine("InGameName=Max"); <-- Setzt einen Key mit Value.
                fileWriter.WriteLine(";"); <-- Setzt einen Kommentar.
                fileWriter.WriteLine(";" + DateTime.Now.ToString()) <-- Setzt einen Kommentar mit Zeitstempel.
                
                
                
                
                VIEL SPAß DAMIT ^^
