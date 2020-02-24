using System.Collections.Generic;
using NameGeneratorMobile.Model;
using Plugin.Share;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Plugin.Share.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;
using static NameGeneratorMobile.Model.FileAccessHelper;

#pragma warning disable CS4014
namespace NameGeneratorMobile.ViewModel {
    class MainViewModel : ViewModelBase {

        public NameGenerator nameGenerator;

        #region Fields
        bool speaking;
        bool areGenerated;
        bool isGirlList;
        bool isMuted;
        string girlNameListPath;
        string boyNameListPath;
        string nameLabel;
        string selectedItem;
        string genderButtonText;
        string soundButtonText;
        string listLabel;
        string background;
        ObservableCollection<string> boyNameList;
        ObservableCollection<string> girlNameList;
        ObservableCollection<string> nameList;

        #endregion

        #region Properties

        public string Background {
            get { return background; }
            set { SetProperty(ref background, value); }
        }

        public string ListLabel {
            get { return listLabel; }
            set { SetProperty(ref listLabel, value); }
        }

        public string GenderButtonText {
            get { return genderButtonText; }
            set { SetProperty(ref genderButtonText, value); }
        }

        public string SoundButtonText {
            get { return soundButtonText; }
            set { SetProperty(ref soundButtonText, value); }
        }

        public string NameLabel {
            get { return nameLabel; }
            set { SetProperty(ref nameLabel, value); }
        }

        public string SelectedItem {
            get { return selectedItem; }
            set {
                if (SetProperty(ref selectedItem, value)) {
                    OnItemSelected();
                }
            }
        }

        public ObservableCollection<string> NameList {
            get { return nameList; }
            set { SetProperty(ref nameList, value); }
        }

        public bool AreGenerated {
            get { return areGenerated; }
            set { SetProperty(ref areGenerated, value); }
        }

        public bool IsGirlList {
            get { return isGirlList; }
            set {
                nameGenerator.isGirl = value;
                SetProperty(ref isGirlList, value);
                if (value) {
                    NameList = girlNameList;
                } else {
                    NameList = boyNameList;
                }
            }
        }

        public bool IsMuted {
            get { return isMuted; }
            set {
                SetProperty(ref isMuted, value);
            }
        }
        #endregion

        public MainViewModel() {
            background = "girlBackground.png";
            App.Current.Resources["ButtonStyle"] = App.Current.Resources["Girl"];
            App.Current.Resources["AltButtonStyle"] = App.Current.Resources["Boy"];
            App.Current.Resources["ListButtonStyle"] = App.Current.Resources["GirlList"];
            App.Current.Resources["HighColor"] = App.Current.Resources["GirlHighColor"];
            App.Current.Resources["LowColor"] = App.Current.Resources["GirlLowColor"];
            girlNameListPath = GetLocalGirlNameListPath();
            boyNameListPath = GetLocalBoyNameListPath();
            nameGenerator = new NameGenerator();
            IsGirlList = true;
            IsMuted = false;
            ListLabel = "Girl Names";
            GenderButtonText = "\uf222";
            SoundButtonText = "\uf028";
            AreGenerated = false;
            Retrieve();
            if (boyNameList == null) {
                boyNameList = new ObservableCollection<string>();
            }
            if (girlNameList == null) {
                girlNameList = new ObservableCollection<string>();
            }
            NameList = girlNameList;
        }

        #region Methods

        private void Retrieve() {

            if (File.Exists(girlNameListPath)) {
                using (StreamReader reader = new StreamReader(girlNameListPath)) {
                    girlNameList = new ObservableCollection<string>();
                    while (!reader.EndOfStream) {
                        girlNameList.Add(reader.ReadLine().Trim());
                    }
                }
            }
            if (File.Exists(boyNameListPath)) {
                using (StreamReader reader = new StreamReader(boyNameListPath)) {
                    boyNameList = new ObservableCollection<string>();
                    while (!reader.EndOfStream) {
                        boyNameList.Add(reader.ReadLine().Trim());
                    }
                }
            }
        }

        public void Stash() {
            using (StreamWriter writer = new StreamWriter(girlNameListPath)) {
                foreach (string name in girlNameList) {
                    writer.WriteLine(name);
                }
            }
            using (StreamWriter writer = new StreamWriter(boyNameListPath)) {
                foreach (string name in boyNameList) {
                    writer.WriteLine(name);
                }
            }
        }

        private async Task Speak(string name) {
            speaking = true;
            await TextToSpeech.SpeakAsync(name);
            speaking = false;
        }

        private void OnItemSelected() {
            if (!speaking) {
                if (SelectedItem != null) {
                    NameLabel = SelectedItem;
                    nameGenerator.PreviousName = nameGenerator.CurrentName;
                    if (!IsMuted) Speak(SelectedItem);
                }

            }
        }

        #endregion

        #region Button Methods

        public Command ToggleGeneratedNames {
            get {
                return new Command(() => {
                    AreGenerated = !AreGenerated;
                });
            }
        }

        public Command GenerateButton {
            get {
                return new Command(() => {
                    if (!speaking) {

                        nameGenerator.PreviousName = nameGenerator.CurrentName;
                        if (AreGenerated) {
                            nameGenerator.CurrentName = nameGenerator.NextName;
                            NameLabel = nameGenerator.CurrentName;
                            if (!IsMuted) Speak(nameGenerator.CurrentName);
                            nameGenerator.NextName = nameGenerator.Generate();
                        } else {
                            nameGenerator.CurrentName = nameGenerator.GetNameFromList();
                            NameLabel = nameGenerator.CurrentName;
                            if (!IsMuted) Speak(NameLabel);
                        }
                    }
                });
            }
        }

        public Command PreviousButton {
            get {
                return new Command(() => {
                    if (!speaking) {
                        if (nameGenerator.PreviousName != null) {
                            NameLabel = nameGenerator.PreviousName;
                            if (!IsMuted) Speak(nameGenerator.PreviousName);
                        }
                    }
                });

            }
        }

        public Command AddButton {
            get {
                return new Command(() => {
                    if (NameLabel != null) {
                        if (!NameList.Contains(NameLabel)) {
                            NameList.Add(NameLabel);
                        }
                    }
                });
            }
        }

        public Command ShareButton {
            get {
                return new Command(() => {
                    if (!CrossShare.IsSupported) {
                        return;
                    }
                    if (NameList.Count > 0) {
                        string saveList = "";
                        foreach (var item in NameList) {
                            saveList += item.ToString() + "\n";
                        }
                        string nameFile = "";
                        string[] nameArr = NameList.ToArray<string>();
                        foreach (string name in nameArr) {
                            nameFile += name + "\n";
                        }
                        CrossShare.Current.Share(new ShareMessage
                        {
                            Title = "List of Names",
                            Text = nameFile
                        }); ;
                    } else {
                        DependencyService.Get<IMessage>().ShortAlert("Add some names to the list first.");
                    }

                });
            }
        }

        public Command ClearButton {
            get {
                return new Command(() => {
                    NameList.Clear();
                });
            }
        }

        public Command GenderButton {
            get {
                return new Command(() => {
                    IsGirlList = !IsGirlList;
                    NameLabel = null;
                    if (IsGirlList) {
                        ListLabel = "Girl Names";
                        GenderButtonText = "\uf222";
                        Background = "girlBackground.png";
                        App.Current.Resources["ButtonStyle"] = App.Current.Resources["Girl"];
                        App.Current.Resources["AltButtonStyle"] = App.Current.Resources["Boy"];
                        App.Current.Resources["ListButtonStyle"] = App.Current.Resources["GirlList"];
                        App.Current.Resources["HighColor"] = App.Current.Resources["GirlHighColor"];
                        App.Current.Resources["LowColor"] = App.Current.Resources["GirlLowColor"];
                    } else {
                        ListLabel = "Boy Names";
                        GenderButtonText = "\uf221";
                        Background = "boyBackground.png";
                        App.Current.Resources["ButtonStyle"] = App.Current.Resources["Boy"];
                        App.Current.Resources["AltButtonStyle"] = App.Current.Resources["Girl"];
                        App.Current.Resources["ListButtonStyle"] = App.Current.Resources["BoyList"];
                        App.Current.Resources["HighColor"] = App.Current.Resources["BoyHighColor"];
                        App.Current.Resources["LowColor"] = App.Current.Resources["BoyLowColor"];
                    }
                });
            }
        }
        public Command RemoveNameButton {
            get {
                return new Command(() => {
                    if (SelectedItem != null) {
                        NameList.Remove(SelectedItem);
                    }
                });
            }
        }
        public Command SoundButton {
            get {
                return new Command(() => {
                    IsMuted = !IsMuted;
                    if (IsMuted) {
                        SoundButtonText = "\uf6a9";
                    } else {
                        SoundButtonText = "\uf028";
                    }
                });
            }
        }

        #endregion




    }
}
