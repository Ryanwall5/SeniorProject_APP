using Rg.Plugins.Popup.Services;
using SA_APP.Models.FromApi;
using SA_APP.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SA_APP.ViewModels
{
    public class StoreItemViewModel
    {
        public ICommand AddToShoppingListCommand => new Command(AddToShoppingList);
        private Item _item;
        private ShoppingUserAPP _user;
        public StoreItemViewModel(Item item, ShoppingUserAPP user)
        {
            _item = item;
            _user = user;

            var htmlSource = new HtmlWebViewSource();
//            htmlSource.Html = @"<html>
//                            <head>
//                                <link rel=""stylesheet"" href=""default.css"">
//                            </head>
//<body>
//<style type=""text/css"">
//      .spoonacular-salmon {
//                color: #e76182
//    }

//    .spoonacular-blue {
//                color: #269fca
//    }

//    .spoonacular-quickview {
//                font-size: 14px;
//                display: inline - block;
//                padding: 5px 10px;
//                border: 1px solid #333;
//        margin-right: 6px;
//                margin-bottom: 6px;
//                font weight: bold
//      }

//    .spoonacular-caption {
//                font-weight: bold;
//                margin-top: 12px;
//                font-size: 16px;
//                margin-bottom: 6px
//      }

//    .spoonacular-nutrition-visualization-bar {
//                display: inline-block;
//                height: 12px;
//                max-width: calc(100 % -70px);
//            }

//        .spoonacular-nutrition-visualization-bar.spoonacular-salmon {
//                background-color: #e76182
//        }

//        .spoonacular-nutrition-visualization-bar.spoonacular-blue {
//                background-color: #269fca
//        }

//    .spoonacular-nutrition-visualization-bar-number {
//                display: inline-block;
//                margin-left: 12px
//            }

//    .spoonacular-nutrient-name {
//                display: inline-block;
//                width: 114px;
//                font - size: 14px
//        }

//    .spoonacular-nutrient-value {
//                display: inline-block;
//                width: 75px;
//                font-size: 14px
//        }

//    .spoonacularNutritionCompositionChart {
//                display: inline-block;
//                z-index: 99999;
//            }

//        .spoonacularNutritionCompositionChart.canvasjs-chart-canvas {
//                border: 1px solid #333;
//            padding: 10px;
//            }
//</style><div class=""spoonacular-caption"">Quickview</div><div class=""spoonacular-quickview"">
//                        180 Calories</div><div class=""spoonacular-quickview"">2g Protein</div><div class=""spoonacular-quickview"">8g Total Fat
//                        </div><div class=""spoonacular-quickview"">25g Carbs</div><div class=""spoonacular-caption spoonacular-salmon"">Limit These</div>
//                        <div class=""spoonacular-nutrient-name"">Calories</div><div class=""spoonacular-nutrient-value"">180</div>
//<div style =""display:inline-block;width: -moz-calc(100% - 189px);width: -webkit-calc(100% - 189px);width: -o-calc(100% - 189px);width: calc(100% - 189px);"">
//<div class=""spoonacular-nutrition-visualization-bar spoonacular-salmon"" style=""width:9.0%"" onmouseover=""spoonacularShowNutritionComposition(event,'ENERGY')""
//onmouseout=""spoonacularHideNutritionComposition('ENERGY')""></div><div class=""spoonacular-nutrition-visualization-bar-number spoonacular-salmon"">9%</div>
//</div><br><div class=""spoonacular-nutrient-name"">Fat</div><div class=""spoonacular-nutrient-value"">8g</div>
//<div style=""display:inline-block;width: -moz-calc(100% - 189px);width: -webkit-calc(100% - 189px);width: -o-calc(100% - 189px);width: calc(100% - 189px);"">
//<div class=""spoonacular-nutrition-visualization-bar spoonacular-salmon"" style=""width:12.31%"" onmouseover=""spoonacularShowNutritionComposition(event,'FAT')"" onmouseout=""spoonacularHideNutritionComposition('FAT')"">
//</div><div class=""spoonacular-nutrition-visualization-bar-number spoonacular-salmon"">12%</div></div><br>
//<div class=""spoonacular-nutrient-name"">&nbsp;&nbsp;Saturated Fat</div><div class=""spoonacular-nutrient-value"">3g</div>
//<div style=""display:inline-block;width: -moz-calc(100% - 189px);width: -webkit-calc(100% - 189px);width: -o-calc(100% - 189px);width: calc(100% - 189px);"">
//<div class=""spoonacular-nutrition-visualization-bar spoonacular-salmon"" style=""width:21.88%"" onmouseover=""spoonacularShowNutritionComposition(event,'FAT_SATURATED')"" onmouseout=""spoonacularHideNutritionComposition('FAT_SATURATED')"">
//</div><div class=""spoonacular-nutrition-visualization-bar-number spoonacular-salmon"">22%</div></div><br><div class=""spoonacular-nutrient-name"">&nbsp;&nbsp;Trans Fat</div>
//<div class=""spoonacular-nutrient-value"">0.5g</div>
//<div style=""display:inline-block;width: -moz-calc(100% - 189px);width: -webkit-calc(100% - 189px);width: -o-calc(100% - 189px);width: calc(100% - 189px);"">
//<div class=""spoonacular-nutrition-visualization-bar spoonacular-salmon"" style=""width:100.0%"" onmouseover=""spoonacularShowNutritionComposition(event,'FAT_TRANS')"" onmouseout=""spoonacularHideNutritionComposition('FAT_TRANS')"">
//</div><div class=""spoonacular-nutrition-visualization-bar-number spoonacular-salmon"">100%</div></div><br><div class=""spoonacular-nutrient-name"">Carbohydrates</div><div class=""spoonacular-nutrient-value"">25g</div><div style=""display:inline-block;width: -moz-calc(100% - 189px);width: -webkit-calc(100% - 189px);width: -o-calc(100% - 189px);width: calc(100% - 189px);"">
//<div class=""spoonacular-nutrition-visualization-bar spoonacular-salmon"" style=""width:8.33%"" onmouseover=""spoonacularShowNutritionComposition(event,'CARBOHYDRATES')"" onmouseout=""spoonacularHideNutritionComposition('CARBOHYDRATES')""></div>
//<div class=""spoonacular-nutrition-visualization-bar-number spoonacular-salmon"">8%</div></div><br><div class=""spoonacular-nutrient-name"">&nbsp;&nbsp;Sugar</div><div class=""spoonacular-nutrient-value"">20g</div>
//<div style=""display:inline-block;width: -moz-calc(100% - 189px);width: -webkit-calc(100% - 189px);width: -o-calc(100% - 189px);width: calc(100% - 189px);"">
//<div class=""spoonacular-nutrition-visualization-bar spoonacular-salmon"" style=""width:22.22%"" onmouseover=""spoonacularShowNutritionComposition(event,'SUGAR')"" onmouseout=""spoonacularHideNutritionComposition('SUGAR')""></div>
//<div class=""spoonacular-nutrition-visualization-bar-number spoonacular-salmon"">22%</div></div><br><div class=""spoonacular-nutrient-name"">Cholesterol</div><div class=""spoonacular-nutrient-value"">5mg</div><div style=""display:inline-block;width: -moz-calc(100% - 189px);width: -webkit-calc(100% - 189px);width: -o-calc(100% - 189px);width: calc(100% - 189px);"">
//<div class=""spoonacular-nutrition-visualization-bar spoonacular-salmon"" style=""width:1.67%"" onmouseover=""spoonacularShowNutritionComposition(event,'CHOLESTEROL')"" onmouseout=""spoonacularHideNutritionComposition('CHOLESTEROL')""></div><div class=""spoonacular-nutrition-visualization-bar-number spoonacular-salmon"">2%</div></div><br><div class=""spoonacular-nutrient-name"">Sodium</div>
//<div class=""spoonacular-nutrient-value"">85mg</div><div style=""display:inline-block;width: -moz-calc(100% - 189px);width: -webkit-calc(100% - 189px);width: -o-calc(100% - 189px);width: calc(100% - 189px);""><div class=""spoonacular-nutrition-visualization-bar spoonacular-salmon"" style=""width:3.7%"" onmouseover=""spoonacularShowNutritionComposition(event,'SODIUM')"" onmouseout=""spoonacularHideNutritionComposition('SODIUM')"">
//</div><div class=""spoonacular-nutrition-visualization-bar-number spoonacular-salmon"">4%</div></div><br><div class=""spoonacular-caption spoonacular-blue"">Get Enough Of These</div>
//<div class=""spoonacular-nutrient-name"">Protein</div><div class=""spoonacular-nutrient-value"">2g</div><div style=""display:inline-block;width: -moz-calc(100% - 189px);width: -webkit-calc(100% - 189px);width: -o-calc(100% - 189px);width: calc(100% - 189px);"">
//<div class=""spoonacular-nutrition-visualization-bar spoonacular-blue"" style=""width:4.0%"" onmouseover=""spoonacularShowNutritionComposition(event,'PROTEIN')"" onmouseout=""spoonacularHideNutritionComposition('PROTEIN')"">
//</div><div class=""spoonacular-nutrition-visualization-bar-number spoonacular-blue"">4%</div></div><br><div class=""spoonacular-nutrient-name"">Calcium</div><div class=""spoonacular-nutrient-value"">40mg</div>
//<div style=""display:inline-block;width: -moz-calc(100% - 189px);width: -webkit-calc(100% - 189px);width: -o-calc(100% - 189px);width: calc(100% - 189px);"">
//<div class=""spoonacular-nutrition-visualization-bar spoonacular-blue"" style=""width:4.0%"" onmouseover=""spoonacularShowNutritionComposition(event,'CALCIUM')"" onmouseout=""spoonacularHideNutritionComposition('CALCIUM')"">
//</div><div class=""spoonacular-nutrition-visualization-bar-number spoonacular-blue"">4%</div></div><br><div class=""spoonacular-nutrient-name"">Fiber</div>
//<div class=""spoonacular-nutrient-value"">1g</div><div style=""display:inline-block;width: -moz-calc(100% - 189px);width: -webkit-calc(100% - 189px);width: -o-calc(100% - 189px);width: calc(100% - 189px);"">
//<div class=""spoonacular-nutrition-visualization-bar spoonacular-blue"" style=""width:4.0%"" onmouseover=""spoonacularShowNutritionComposition(event,'FIBER')"" onmouseout=""spoonacularHideNutritionComposition('FIBER')""></div>
//<div class=""spoonacular-nutrition-visualization-bar-number spoonacular-blue"">4%</div></div><br><div class=""spoonacular-nutrient-name"">Iron</div><div class=""spoonacular-nutrient-value"">0.36mg</div>
//<div style=""display:inline-block;width: -moz-calc(100% - 189px);width: -webkit-calc(100% - 189px);width: -o-calc(100% - 189px);width: calc(100% - 189px);""><div class=""spoonacular-nutrition-visualization-bar spoonacular-blue"" style=""width:2.0%"" onmouseover=""spoonacularShowNutritionComposition(event,'IRON')"" onmouseout=""spoonacularHideNutritionComposition('IRON')""></div>
//<div class=""spoonacular-nutrition-visualization-bar-number spoonacular-blue"">2%</div></div><br><div style = ""margin-top:12px;width:12px;height:12px"" class=""spoonacular-nutrition-visualization-bar spoonacular-salmon""></div><div style = ""margin-left:6px;margin-right:6px;width:12px;height:12px"" class=""spoonacular-nutrition-visualization-bar spoonacular-blue""></div>covered percent of daily need</body></html>";


            string head = @"<html>
                            <head>
                                <link rel=""stylesheet"" href=""default.css"">
                            </head>
<body>
<style type=""text/css"">
      .spoonacular-salmon {
                color: #e76182
    }

    .spoonacular-blue {
                color: #269fca
    }

    .spoonacular-quickview {
                font-size: 14px;
                display: inline - block;
                padding: 5px 10px;
                border: 1px solid #333;
        margin-right: 6px;
                margin-bottom: 6px;
                font weight: bold
      }

    .spoonacular-caption {
                font-weight: bold;
                margin-top: 12px;
                font-size: 16px;
                margin-bottom: 6px
      }

    .spoonacular-nutrition-visualization-bar {
                display: inline-block;
                height: 12px;
                max-width: calc(100 % -70px);
            }

        .spoonacular-nutrition-visualization-bar.spoonacular-salmon {
                background-color: #e76182
        }

        .spoonacular-nutrition-visualization-bar.spoonacular-blue {
                background-color: #269fca
        }

    .spoonacular-nutrition-visualization-bar-number {
                display: inline-block;
                margin-left: 12px
            }

    .spoonacular-nutrient-name {
                display: inline-block;
                width: 114px;
                font - size: 14px
        }

    .spoonacular-nutrient-value {
                display: inline-block;
                width: 75px;
                font-size: 14px
        }

    .spoonacularNutritionCompositionChart {
                display: inline-block;
                z-index: 99999;
            }

        .spoonacularNutritionCompositionChart.canvasjs-chart-canvas {
                border: 1px solid #333;
            padding: 10px;
            }
</style>";
            string body = @"<div itemprop =""nutrition"" itemscope itemtype=""http://schema.org/NutritionInformation""><div class=""spoonacular-caption"">Quickview</div><div class=""spoonacular-quickview"" itemprop=""calories"">150 Calories</div><div class=""spoonacular-quickview"" itemprop=""proteinContent"">4g Protein</div><div class=""spoonacular-quickview"" itemprop=""fatContent"">13g Total Fat</div><div class=""spoonacular-quickview"" itemprop=""carbohydrateContent"">4g Carbs</div></div><div class=""spoonacular-caption spoonacular-salmon"">Limit These</div><div class=""spoonacular-nutrient-name"">Calories</div><div class=""spoonacular-nutrient-value"">150</div><div style=""display:inline-block;width: -moz-calc(100% - 189px);width: -webkit-calc(100% - 189px);width: -o-calc(100% - 189px);width: calc(100% - 189px);""><div class=""spoonacular-nutrition-visualization-bar spoonacular-salmon"" style=""width:7.5%"" onmouseover=""spoonacularShowNutritionComposition(event,'ENERGY')"" onmouseout=""spoonacularHideNutritionComposition('ENERGY')""></div><div class=""spoonacular-nutrition-visualization-bar-number spoonacular-salmon"">8%</div></div><br><div class=""spoonacular-nutrient-name"">Fat</div><div class=""spoonacular-nutrient-value"">13g</div><div style=""display:inline-block;width: -moz-calc(100% - 189px);width: -webkit-calc(100% - 189px);width: -o-calc(100% - 189px);width: calc(100% - 189px);""><div class=""spoonacular-nutrition-visualization-bar spoonacular-salmon"" style=""width:20.0%"" onmouseover=""spoonacularShowNutritionComposition(event,'FAT')"" onmouseout=""spoonacularHideNutritionComposition('FAT')""></div><div class=""spoonacular-nutrition-visualization-bar-number spoonacular-salmon"">20%</div></div><br><div class=""spoonacular-nutrient-name"">&nbsp;&nbsp;Saturated Fat</div><div class=""spoonacular-nutrient-value"">4g</div><div style=""display:inline-block;width: -moz-calc(100% - 189px);width: -webkit-calc(100% - 189px);width: -o-calc(100% - 189px);width: calc(100% - 189px);""><div class=""spoonacular-nutrition-visualization-bar spoonacular-salmon"" style=""width:28.13%"" onmouseover=""spoonacularShowNutritionComposition(event,'FAT_SATURATED')"" onmouseout=""spoonacularHideNutritionComposition('FAT_SATURATED')""></div><div class=""spoonacular-nutrition-visualization-bar-number spoonacular-salmon"">28%</div></div><br><div class=""spoonacular-nutrient-name"">Carbohydrates</div><div class=""spoonacular-nutrient-value"">4g</div><div style=""display:inline-block;width: -moz-calc(100% - 189px);width: -webkit-calc(100% - 189px);width: -o-calc(100% - 189px);width: calc(100% - 189px);""><div class=""spoonacular-nutrition-visualization-bar spoonacular-salmon"" style=""width:1.33%"" onmouseover=""spoonacularShowNutritionComposition(event,'CARBOHYDRATES')"" onmouseout=""spoonacularHideNutritionComposition('CARBOHYDRATES')""></div><div class=""spoonacular-nutrition-visualization-bar-number spoonacular-salmon"">1%</div></div><br><div class=""spoonacular-nutrient-name"">&nbsp;&nbsp;Sugar</div><div class=""spoonacular-nutrient-value"">3g</div><div style=""display:inline-block;width: -moz-calc(100% - 189px);width: -webkit-calc(100% - 189px);width: -o-calc(100% - 189px);width: calc(100% - 189px);""><div class=""spoonacular-nutrition-visualization-bar spoonacular-salmon"" style=""width:3.33%"" onmouseover=""spoonacularShowNutritionComposition(event,'SUGAR')"" onmouseout=""spoonacularHideNutritionComposition('SUGAR')""></div><div class=""spoonacular-nutrition-visualization-bar-number spoonacular-salmon"">3%</div></div><br><div class=""spoonacular-nutrient-name"">Cholesterol</div><div class=""spoonacular-nutrient-value"">35mg</div><div style=""display:inline-block;width: -moz-calc(100% - 189px);width: -webkit-calc(100% - 189px);width: -o-calc(100% - 189px);width: calc(100% - 189px);""><div class=""spoonacular-nutrition-visualization-bar spoonacular-salmon"" style=""width:11.67%"" onmouseover=""spoonacularShowNutritionComposition(event,'CHOLESTEROL')"" onmouseout=""spoonacularHideNutritionComposition('CHOLESTEROL')""></div><div class=""spoonacular-nutrition-visualization-bar-number spoonacular-salmon"">12%</div></div><br><div class=""spoonacular-nutrient-name"">Sodium</div><div class=""spoonacular-nutrient-value"">550mg</div><div style=""display:inline-block;width: -moz-calc(100% - 189px);width: -webkit-calc(100% - 189px);width: -o-calc(100% - 189px);width: calc(100% - 189px);""><div class=""spoonacular-nutrition-visualization-bar spoonacular-salmon"" style=""width:23.91%"" onmouseover=""spoonacularShowNutritionComposition(event,'SODIUM')"" onmouseout=""spoonacularHideNutritionComposition('SODIUM')""></div><div class=""spoonacular-nutrition-visualization-bar-number spoonacular-salmon"">24%</div></div><br><div class=""spoonacular-caption spoonacular-blue"">Get Enough Of These</div><div class=""spoonacular-nutrient-name"">Protein</div><div class=""spoonacular-nutrient-value"">4g</div><div style=""display:inline-block;width: -moz-calc(100% - 189px);width: -webkit-calc(100% - 189px);width: -o-calc(100% - 189px);width: calc(100% - 189px);""><div class=""spoonacular-nutrition-visualization-bar spoonacular-blue"" style=""width:8.0%"" onmouseover=""spoonacularShowNutritionComposition(event,'PROTEIN')"" onmouseout=""spoonacularHideNutritionComposition('PROTEIN')""></div><div class=""spoonacular-nutrition-visualization-bar-number spoonacular-blue"">8%</div></div><br><div class=""spoonacular-nutrient-name"">Calcium</div><div class=""spoonacular-nutrient-value"">20mg</div><div style=""display:inline-block;width: -moz-calc(100% - 189px);width: -webkit-calc(100% - 189px);width: -o-calc(100% - 189px);width: calc(100% - 189px);""><div class=""spoonacular-nutrition-visualization-bar spoonacular-blue"" style=""width:2.0%"" onmouseover=""spoonacularShowNutritionComposition(event,'CALCIUM')"" onmouseout=""spoonacularHideNutritionComposition('CALCIUM')""></div><div class=""spoonacular-nutrition-visualization-bar-number spoonacular-blue"">2%</div></div><br><div class=""spoonacular-nutrient-name"">Iron</div><div class=""spoonacular-nutrient-value"">0.36mg</div><div style=""display:inline-block;width: -moz-calc(100% - 189px);width: -webkit-calc(100% - 189px);width: -o-calc(100% - 189px);width: calc(100% - 189px);""><div class=""spoonacular-nutrition-visualization-bar spoonacular-blue"" style=""width:2.0%"" onmouseover=""spoonacularShowNutritionComposition(event,'IRON')"" onmouseout=""spoonacularHideNutritionComposition('IRON')""></div><div class=""spoonacular-nutrition-visualization-bar-number spoonacular-blue"">2%</div></div><br><div style=""margin-top:12px;width:12px;height:12px"" class=""spoonacular-nutrition-visualization-bar spoonacular-salmon""></div><div style=""margin-left:6px;margin-right:6px;width:12px;height:12px"" class=""spoonacular-nutrition-visualization-bar spoonacular-blue""></div>covered percent of daily need";

            string end = @"</body></html>";

            htmlSource.Html = head + item.ProductInformation.nutrition_widget + end;


            _item.NutritionWidget = htmlSource;



            if(_item.ProductInformation.generated_text == "" || _item.ProductInformation.generated_text == null)
            {
                _item.ProductInformation.generated_text = "No product information";
            }
            if (_item.ProductInformation.serving_size == "" || _item.ProductInformation.serving_size == null)
            {
                _item.ProductInformation.serving_size = "No serving size";
            }
        }

        public string GeneratedText => _item.ProductInformation.generated_text;
        public string Name => _item.Name;
        public HtmlWebViewSource NutritionWidget => _item.NutritionWidget;
        public string ServingSize => _item.ProductInformation.serving_size;
        public string Image => _item.Image;

        public async void AddToShoppingList()
        {
            await PopupNavigation.Instance.PushAsync(new AddItemToListPopup(_item, _user));
        }
    }
}
