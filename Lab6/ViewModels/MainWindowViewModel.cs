using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using Lab6.Models;
using System.Reactive.Linq;
using System.Reactive;
using System.Collections.ObjectModel;

namespace Lab6.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        ViewModelBase currentWindow;
        public MainWindowViewModel()
        {
            CurrentWindow = PlannerWindow = new PlannerViewModel();
            NotesList = new ObservableCollection<Note>();
        }
        public ViewModelBase CurrentWindow
        {
            set => this.RaiseAndSetIfChanged(ref currentWindow, value);
            get => currentWindow;
        }
        public PlannerViewModel PlannerWindow
        {
            get;
        }
        bool isTitle = false;
        public bool IsTitleExist
        {
            get => isTitle;
            set => this.RaiseAndSetIfChanged(ref isTitle, value);
        }



        Dictionary<DateTimeOffset, ObservableCollection<Note>> notesDictionary = new Dictionary<DateTimeOffset, ObservableCollection<Note>>();

        string currentTitle;
        public string CurrentTitle
        {
            get => currentTitle;
            set => this.RaiseAndSetIfChanged(ref currentTitle, value);
        }
        string currentDescription;
        public string CurrentDescription
        {
            get => currentDescription;
            set => this.RaiseAndSetIfChanged(ref currentDescription, value);
        }

        DateTimeOffset selectedDate = DateTimeOffset.Now.Date;
        public DateTimeOffset SelectedDate
        {
            get => selectedDate;
            set
            {
                this.RaiseAndSetIfChanged(ref selectedDate, value);
                UpdateNotes();
            }
        }
        public void UpdateNotes()
        {
            NotesList.Clear();
            foreach (var note in notesDictionary[SelectedDate])
            {
                NotesList.Add(note);
            }
        }

        ObservableCollection<Note> notesList;
        public ObservableCollection<Note> NotesList
        {
            get => notesList;
            set => this.RaiseAndSetIfChanged(ref notesList, value);
        }


        
        public void AddNewNote()
        {
            if(CurrentTitle != null)
            {
                if(CurrentTitle.Length > 0)
                {
                    if (notesDictionary.ContainsKey(SelectedDate) == false)
                    {
                        notesDictionary.Add(SelectedDate, new ObservableCollection<Note>());
                    }
                    
                    bool isNoteAlreadyExist = false;
                    foreach(var note in notesDictionary[SelectedDate])
                    {
                        if(note.Title == CurrentTitle)
                        {
                            isNoteAlreadyExist = true;
                            note.Description = CurrentDescription;
                        }
                    }

                    if(isNoteAlreadyExist)
                    {
                        
                    }
                    else
                    {
                        var NewNote = new Note(CurrentTitle, CurrentDescription);
                        notesDictionary[SelectedDate].Add(NewNote);
                    }
                    


                    UpdateNotes();


                    ChangeWindow();
                }
            }          
        }

        public void EditNote(Note thisNote)
        {

            CurrentTitle = thisNote.Title;
            CurrentDescription = thisNote.Description;

            ChangeWindow();
        }

        void RemoveNote(Note thisNote)
        {
            notesDictionary[SelectedDate].Remove(thisNote);
            UpdateNotes();
        }

        public void ClrChange()
        {

            CurrentTitle = null;
            CurrentDescription = null;

            ChangeWindow();
        }

        public void ChangeWindow()
        {
            if (currentWindow is PlannerViewModel)
            {
                CurrentWindow = new NoteViewModel();
            }
            else if (currentWindow is NoteViewModel)
            {
                CurrentWindow = new PlannerViewModel();
            }
        }





        //public void AddNote()
        //{
        //    var temp = new NoteViewModel();

        //    Observable.Merge(temp.buttonOk, temp.buttonCancel.Select(_ => (Note)null))
        //        .Take(1)
        //        .Subscribe((returnedNote) =>
        //        {
        //            if (returnedNote != null)
        //            {
        //                PlannerWindow.Notes.Add(returnedNote);
        //            }
        //            CurrentWindow = PlannerWindow;
        //        }
        //        );

        //    CurrentWindow = temp;
        //}




    }
}