using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using static NameGeneratorMobile.Model.FileAccessHelper;


namespace NameGeneratorMobile.Model {
    class NameGenerator {
        string[] NameParts;
        public string CurrentName { get; set; }
        public string PreviousName { get; set; }
        public string NextName { get; set; }
        Random rand;
        int parts;
        int one;
        int two;
        int three;
        public bool isGirl { get; set; }
        ObservableCollection<string> girlNames;
        ObservableCollection<string> boyNames;
        SQLiteAsyncConnection con;

        public NameGenerator() {
            girlNames = new ObservableCollection<string>();
            boyNames = new ObservableCollection<string>();
            isGirl = true;
            rand = new Random();
            SetGender();
            //NextName = Generate();
            con = new SQLiteAsyncConnection(GetLocalDBFilePath("Names.db3"));

        }

        public async Task GetNamesAsync() {
            List<GirlName> gnames = await con.Table<GirlName>().ToListAsync();
            List<BoyName> bnames = await con.Table<BoyName>().ToListAsync();
            foreach (var item in gnames) {
                girlNames.Add(item.Name);
            }
            foreach (var item in bnames) {
                boyNames.Add(item.Name);
            }

        }



        private void RandomForGirl() {
            one = rand.Next(0, 17);
            two = rand.Next(17, 25);
            three = rand.Next(25, NameParts.Length);
        }

        private void RandomForBoy() {
            //change
            one = rand.Next(0, 17);
            two = rand.Next(17, 25);
            three = rand.Next(25, NameParts.Length);
        }

        public string Generate() {
            string tempName;
            parts = rand.Next(2, 4);
            if (isGirl) {
                RandomForGirl();
            } else {
                RandomForBoy();
            }
            do {
                if (parts == 2) {
                    tempName = NameParts[one] + NameParts[three];

                } else {
                    tempName = NameParts[one] + NameParts[two] + NameParts[three];
                }

                tempName = char.ToUpper(tempName[0]) + tempName.Substring(1);
            }
            while (tempName.ToLower().Contains("aa") || tempName.ToLower().Contains("yy")
                  || tempName.ToLower().Contains("ii") || tempName.ToLower().Contains("ee")
                  || tempName.ToLower().Contains("oo") || tempName.ToLower().Contains("uu"));
            return tempName;
        }

        public void SetGender() {
            if (isGirl) {
                NameParts = new [] { "a", "o", "ar", "ky", "je", "ba", "bri", "val", "vi", "de", "cor", "em", "av", "dor", "ven", "dar", "sil",
            "el", "rin",  "ne", "ri", "or",  "a", "ra", "yn",
            "ry", "sha", "is", "ma", "ette","lyn",
            "etta", "ella", "ica", "essa", "ina", "cia", "anna", "ia" };
                //set names[]
            } else {
                NameParts = null;
                //set all, names[] and parts
            }
        }


        public string GetNameFromList() {
            if (isGirl) {
                return girlNames[rand.Next(0, girlNames.Count)];
            } else {
                return boyNames[rand.Next(0, boyNames.Count)];
            }
        }
    }
}
