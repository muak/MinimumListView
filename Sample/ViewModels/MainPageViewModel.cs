using System;
using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace Sample.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        public ObservableCollection<Person> Items { get; set; }

        public MainPageViewModel()
        {
            Items = new ObservableCollection<Person>();
            var rand = new Random();

            for (var i = 0; i < 150; i++) {
                Items.Add(
                    new Person {
                        Name = RandomName(rand),
                        Age = rand.Next(99)
                    }
                );
            }
        }

        string RandomName(Random rand)
        {
            var FirstName = new char[rand.Next(3, 10)];

            for (var i = 0; i < FirstName.Length; i++) {
                var c = i == 0 ? 'A' : 'a';
                FirstName[i] = (char)(c + rand.Next(0, 25));
            }

            var LastName = new char[rand.Next(3, 10)];

            for (var i = 0; i < LastName.Length; i++) {
                var c = i == 0 ? 'A' : 'a';
                LastName[i] = (char)(c + rand.Next(0, 25));
            }

            return new string(FirstName) + " " + new string(LastName);
        }

        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}

