using PolSourceBackendDeveloperAssignment.Models;
using System;

namespace PolSourceBackendDeveloperAssigment.IntegrationTests
{
    class SeedData
    {
        internal static void AddData(NotesContext appDb)
        {
            appDb.Notes.Add(new Note { IdNote = 1, Title = "To jest pierwsza notatka", Content = "To jest zawartosc pierwszej notatki", Created = DateTime.Today });
            appDb.Notes.Add(new Note { IdNote = 3, Title = "To jest trzecia notatka", Content = "To jest zawartosc trzeciej notatki", Created = DateTime.Today });
            appDb.Notes.Add(new Note { IdNote = 4, Title = "To jest czwarta notatka", Content = "To jest zawartosc czwartej notatki", Created = DateTime.Today });
            appDb.Notes.Add(new Note { IdNote = 9, Title = "To jest dziewiata notatka", Content = "To jest zawartosc dziewiatej notatki", Created = DateTime.Today });
            appDb.Notes.Add(new Note { IdNote = 17, Title = "To jest siedemnasta notatka", Content = "To jest zawartosc siedemnastej notatki", Created = DateTime.Today });
            appDb.Notes.Add(new Note { IdNote = 18, Title = "To jest siedemnasta notatka", Content = "To jest zawartosc siedemnastej notatki", Created = DateTime.Today });


            appDb.NoteHistories.Add(new NoteHistory { IdNote = 1, Version = 1 , Title = "To jest pierwsza notatka", Content = "To jest zawartosc pierwszej notatki", Created = DateTime.Today, Modified = DateTime.Today });
            appDb.NoteHistories.Add(new NoteHistory { IdNote = 9, Version = 1 , Title = "To jest czwarta notatka", Content = "To jest zawartosc czwartej notatki", Created = DateTime.Today, Modified = DateTime.Today });
            appDb.NoteHistories.Add(new NoteHistory { IdNote = 4, Version = 1, Title = "To jest dzie notatka", Content = "To jest zawartosc dziewiatej notatki", Created = DateTime.Today, Modified = DateTime.Today });
            appDb.NoteHistories.Add(new NoteHistory { IdNote = 18, Version = 1, Title = "To jest siedemnasta notatka", Content = "To jest zawartosc siedemnastej notatki", Created = DateTime.Today, Modified = DateTime.Today });
            appDb.NoteHistories.Add(new NoteHistory { IdNote = 18, Version = 2, Title = "To jest notatka", Content = "To jest zawartosc notatki", Created = DateTime.Today, Modified = DateTime.Today.AddDays(2) });

            appDb.SaveChanges();
        }
    }
}
