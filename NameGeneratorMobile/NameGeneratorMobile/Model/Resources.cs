using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NameGeneratorMobile.Model {
    class Resources {
        public ResourceDictionary RS { get; set; }

        public Resources() {

            Style BaseListButton = new Style(typeof(Button))
            {
                Setters = {
                    new Setter { Property = Button.CornerRadiusProperty, Value = "30" },
                    new Setter { Property = Button.BorderWidthProperty, Value="3" },
                    new Setter { Property = Button.PaddingProperty,  Value = "10" },
                }
            };


            Style BaseButton = new Style(typeof(Button))
            {
                Setters = {
                    new Setter { Property = Button.CornerRadiusProperty, Value = "23" },
                    new Setter { Property = Button.BorderWidthProperty, Value="5" },
                    new Setter { Property = Button.WidthRequestProperty,  Value = "65" },
                    new Setter { Property = Button.HeightRequestProperty,  Value = "65" },
                    new Setter { Property = Button.MarginProperty, Value = "0, 6, 20, 6"}
                }
            };

            Style Girl = new Style(typeof(Button)) { 
                BasedOn = BaseButton,
                Setters = {
                    new Setter { Property = Button.TextColorProperty, Value = "#ff69b4" },
                    new Setter { Property = Button.BackgroundColorProperty, Value="#ffd5e5" },
                    new Setter { Property = Button.BorderColorProperty,  Value = "#ff69b4" },
                }
            };

            Style Boy = new Style(typeof(Button))
            {
                BasedOn = BaseButton,
                Setters = {
                    new Setter { Property = Button.TextColorProperty, Value = "#5386d3" },
                    new Setter { Property = Button.BackgroundColorProperty, Value="#bad4fd" },
                    new Setter { Property = Button.BorderColorProperty,  Value = "#5386d3" },
                }
            };

            Style GirlList = new Style(typeof(Button))
            {
                BasedOn = BaseListButton,
                Setters = {
                    new Setter { Property = Button.TextColorProperty, Value = "HotPink" },
                    new Setter { Property = Button.BackgroundColorProperty, Value="#ffd5e5" },
                    new Setter { Property = Button.BorderColorProperty,  Value = "HotPink" },
                }
            };

            Style BoyList = new Style(typeof(Button))
            {
                BasedOn = BaseListButton,
                Setters = {
                    new Setter { Property = Button.TextColorProperty, Value = "#5386d3" },
                    new Setter { Property = Button.BackgroundColorProperty, Value="#bad4fd" },
                    new Setter { Property = Button.BorderColorProperty,  Value = "#5386d3" },
                }
            };

            Color GirlHighColor = Color.FromHex("#ff69b4");
            Color GirlLowColor = Color.FromHex("#ffd5e5");
            Color BoyHighColor = Color.FromHex("#5386d3");
            Color BoyLowColor = Color.FromHex("#bad4fd");

            RS = new ResourceDictionary();
            RS.Add("Girl", Girl);
            RS.Add("Boy", Boy);
            RS.Add("GirlList", GirlList);
            RS.Add("BoyList", BoyList);
            RS.Add("GirlHighColor", GirlHighColor);
            RS.Add("GirlLowColor", GirlLowColor);
            RS.Add("BoyHighColor", BoyHighColor);
            RS.Add("BoyLowColor", BoyLowColor);
        }

    }
}
