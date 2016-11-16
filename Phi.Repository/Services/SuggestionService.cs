/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.Repository.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Phi.Models.Models;
    using Phi.Repository.Enums;
    using Phi.Repository.Extensions;
    using Phi.Repository.Stores;

    /// <summary>
    /// 
    /// </summary>
    public class SuggestionService : ISuggestionService
    {
        #region Private constants

        /// <summary>
        /// The tolerance.
        /// </summary>
        private const Double TOLERANCE = 0.001;

        private readonly Double[] _temperatures = { 21, 24, 26.5, 29.5, 32, 35, 38, 40.5, 43.3, 46.1, 48.9 };
        private readonly Double[] _humidity = { 0, 10, 20, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100 };
        private readonly Double[,] _effectiveTemperaturesViaHumidity =
            {
                { 18, 18.5, 19, 19.5, 19.5, 20, 20, 20.5, 20.5, 21, 21, 21, 21, 21.5, 21.5, 21.5, 21.5, 22 },
                { 20.5, 21, 22, 22.5, 22.5, 23, 23, 24, 24, 24.5, 24.5, 25, 25, 25.5, 25.5, 26, 26, 26.5 },
                { 23, 24, 25, 25.5, 26, 26, 26.5, 27, 27, 28, 28.5, 29.5, 30, 30, 30.5, 31, 31.5, 32.5 },
                { 25.5, 26.5, 28, 29, 29.5, 30, 31, 31.5, 32, 32.5, 33, 34, 35, 36, 37, 39, 41, 42.5 },
                { 28, 29.5, 30.5, 32.5, 33, 34, 35, 36, 37, 38, 39, 41, 43, 45, 47.5, 50, -273, -273 },
                { 30.5, 32, 34, 36, 38.5, 40, 42, 43.5, 46, 48.5, 51, 54.5, 58, -273, -273, -273, -273, -273 },
                { 33, 35, 37, 40, 42, 43.5, 46, 49, 52.5, 56, 59, 62.5, -273, -273, -273, -273, -273, -273 },
                { 35, 38, 40.5, 45, 48, 51, 54, 57.5, 61, 65, -273, -273, -273, -273, -273, -273, -273, -273 },
                { 37, 40.5, 44.5, 51, 54.5, 58.5, 62, 66, -273, -273, -273, -273, -273, -273, -273, -273, -273, -273 },
                { 39.5, 44, 49, 57.5, 62, 66, -273, -273, -273, -273, -273, -273, -273, -273, -273, -273, -273, -273 },
                { 41.5, 46.6, 54.5, 64.5, -273, -273, -273, -273, -273, -273, -273, -273, -273, -273, -273, -273, -273, -273 }
            };

        private readonly Dictionary<int, int> commonSuggestionTerms = new Dictionary<int, int>
        {
            { 1, -3525 },
            { 2, -2515 },
            { 4, -1510 },
            { 8, -105 },
            { 16, -51 },
            { 32, 15 },
            { 64, 510 },
            { 128, 1015 },
            { 256, 1518 },
            { 512, 1822 },
            { 1024, 2227 },
            { 2048, 2732 },
            { 4096, 3235 }
        };

        private readonly Dictionary<CommonSuggestionType, List<CommonSuggestionItem>> commonItemsRUS = new Dictionary<CommonSuggestionType, List<CommonSuggestionItem>>
        {
            { CommonSuggestionType.Child03, new List<CommonSuggestionItem>
                {
                    new CommonSuggestionItem { Name = "Памперс", Term = 8191 },
                    new CommonSuggestionItem { Name = "Слип (хлопковый комбинезон с закрытыми ножками и иногда ручками) или боди с длинными рукавами и штанишки с носками (хлопок или фланель)", Term = 508 },
                    new CommonSuggestionItem { Name = "Шерстяной или фланелевый комбинезон", Term = 12 },
                    new CommonSuggestionItem { Name = "Флисовый конверт или мешок", Term = 56 },
                    new CommonSuggestionItem { Name = "Тонкая шапочка", Term = 496 },
                    new CommonSuggestionItem { Name = "Плотная шапочка", Term = 12 },
                    new CommonSuggestionItem { Name = "Конверт-мешок на овчине", Term = 4 },
                    new CommonSuggestionItem { Name = "Одеяло флисовое или байковое", Term = 508 },
                    new CommonSuggestionItem { Name = "Хлопковый конверт или мешок", Term = 192 },
                    new CommonSuggestionItem { Name = "Одноразовая пеленка", Term = 6144 },
                    new CommonSuggestionItem { Name = "Панамка от солнца (если ребенок гуляет в слинге или на руках)", Term = 7680 },
                    new CommonSuggestionItem { Name = "Тонкая шапочка/чепчик",Term = 256 },
                    new CommonSuggestionItem { Name = "Боди/футболка/распашонка или сарафан",Term = 3584 },
                    new CommonSuggestionItem { Name = "Легкий плед или пеленка",Term = 7936 }
                }
            },
            { CommonSuggestionType.Child36, new List<CommonSuggestionItem>
                {
                    new CommonSuggestionItem { Name = "Памперс", Term = 8191 },
                    new CommonSuggestionItem { Name = "Слип (хлопковый комбинезон с закрытыми ножками и иногда ручками) или боди с длинными рукавами и штанишки (хлопок или фланель)", Term = 380 },
                    new CommonSuggestionItem { Name = "Шерстяная поддева", Term = 12 },
                    new CommonSuggestionItem { Name = "Флисовый конверт или комбинезон", Term = 56 },
                    new CommonSuggestionItem { Name = "Тонкая шапочка", Term = 248 },
                    new CommonSuggestionItem { Name = "Плотная шапочка", Term = 12 },
                    new CommonSuggestionItem { Name = "Конверт-комбинезон на овчине или пуху + варежки и валенки на носок из шерсти", Term = 6 },
                    new CommonSuggestionItem { Name = "Одеяло флисовое или байковое", Term = 508 },
                    new CommonSuggestionItem { Name = "Боди с длинными рукавами", Term = 384 },
                    new CommonSuggestionItem { Name = "Водолазка (трикотаж)", Term = 128 },
                    new CommonSuggestionItem { Name = "Толстовка (хлопок, флис) или кофточка с капюшоном", Term = 448 },
                    new CommonSuggestionItem { Name = "Брюки (хлопок, флис, вельвет) на мягкой резинке или джинсы с носками", Term = 448 },
                    new CommonSuggestionItem { Name = "Трусики х/б и пеленка", Term = 6144 },
                    new CommonSuggestionItem { Name = "Панамка от солнца (если ребенок гуляет в слинге или на руках)", Term = 7680 },
                    new CommonSuggestionItem { Name = "Тонкая шапочка/чепчик", Term = 256 },
                    new CommonSuggestionItem { Name = "Боди/футболка/распашонка или сарафан", Term = 3584 },
                    new CommonSuggestionItem { Name = "Легкий плед или пеленка", Term = 7936 },
                }
            },
            { CommonSuggestionType.Child69, new List<CommonSuggestionItem>
                {
                    new CommonSuggestionItem { Name = "Памперс", Term = 2047 },
                    new CommonSuggestionItem { Name = "Боди с рукавами", Term = 255 },
                    new CommonSuggestionItem { Name = "Колготки", Term = 127 },
                    new CommonSuggestionItem { Name = "Шерстяная или флисовая поддева", Term = 7 },
                    new CommonSuggestionItem { Name = "Комбинезон и варежки", Term = 63 },
                    new CommonSuggestionItem { Name = "Тонкая шапочка", Term = 248 },
                    new CommonSuggestionItem { Name = "Плотная шапочка (шлем)", Term = 7 },
                    new CommonSuggestionItem { Name = "Носки и валенки или ботинки на овчине, если нет меховых пинеток на комбинезоне", Term = 63 },
                    new CommonSuggestionItem { Name = "Одеяло флисовое или байковое", Term = 511 },
                    new CommonSuggestionItem { Name = "Водолазка (трикотаж)", Term = 192 },
                    new CommonSuggestionItem { Name = "Толстовка (хлопок, флис) или непромокаемая куртка с капюшоном на случай дождя", Term = 448 },
                    new CommonSuggestionItem { Name = "Брюки (хлопок, флис, вельвет) на мягкой резинке или джинсы", Term = 448 },
                    new CommonSuggestionItem { Name = "Шерстяные носки или утепленные пинетки (ботинки)", Term = 240 },
                    new CommonSuggestionItem { Name = "Легкие ботинки или носки", Term = 256 },
                    new CommonSuggestionItem { Name = "Трусики х/б и пеленка", Term = 6144 },
                    new CommonSuggestionItem { Name = "Слип (хлопковый комбинезон с закрытыми ножками и иногда ручками) или боди и штанишки с носками", Term = 256 },
                    new CommonSuggestionItem { Name = "Панамка от солнца (если ребенок гуляет в слинге или на руках)", Term = 7680 },
                    new CommonSuggestionItem { Name = "Тонкая шапочка/чепчик", Term = 256 },
                    new CommonSuggestionItem { Name = "Боди/футболка/распашонка или сарафан", Term = 3584 },
                    new CommonSuggestionItem { Name = "Легкий плед или пеленка", Term = 7936 }
                }
            },
            { CommonSuggestionType.Child912, new List<CommonSuggestionItem>
                {
                    new CommonSuggestionItem { Name = "Памперс", Term = 1023 },
                    new CommonSuggestionItem { Name = "Трусики х/б", Term = 7168 },
                    new CommonSuggestionItem { Name = "Майка (тонкая футболка или боди с рукавами", Term = 511 },
                    new CommonSuggestionItem { Name = "Колготки", Term = 127 },
                    new CommonSuggestionItem { Name = "Водолазка (хлопок)", Term = 4 },
                    new CommonSuggestionItem { Name = "Шерстяная или флисовая поддева", Term = 7 },
                    new CommonSuggestionItem { Name = "Комбинезон и варежки (лучше непромокаемые)", Term = 63 },
                    new CommonSuggestionItem { Name = "Тонкая шапочка", Term = 248 },
                    new CommonSuggestionItem { Name = "Плотная шапочка (шлем)", Term = 7 },
                    new CommonSuggestionItem { Name = "Валенки или непромокаемые ботинки", Term = 63 },
                    new CommonSuggestionItem { Name = "Водолазка (трикотаж)", Term = 192 },
                    new CommonSuggestionItem { Name = "Толстовка (хлопок, флис) или непромокаемая куртка с капюшоном на случай дождя", Term = 448 },
                    new CommonSuggestionItem { Name = "Брюки (хлопок, флис, вельвет) на мягкой резинке или джинсы", Term = 448 },
                    new CommonSuggestionItem { Name = "Утепленные непромокаемые ботинки", Term = 112 },
                    new CommonSuggestionItem { Name = "Легкие ботинки или резиновые сапоги на случай дождя", Term = 384 },
                    new CommonSuggestionItem { Name = "Панамка от солнца", Term = 7680 },
                    new CommonSuggestionItem { Name = "Толстовка, футболка с длинным или коротким рукавом, штаны, носки", Term = 256 },
                    new CommonSuggestionItem { Name = "Песочник/ футболка и шорты/юбка или сарафан/платье", Term = 7680 },
                    new CommonSuggestionItem { Name = "Ботинки/сандалии (в коляске не нужны)", Term = 7936 },
                    new CommonSuggestionItem { Name = "Худи", Term = 7936 }
                }
            },
            { CommonSuggestionType.Child1218, new List<CommonSuggestionItem>
                {
                    new CommonSuggestionItem { Name = "Памперс (или трусики х/б)", Term = 1023 },
                    new CommonSuggestionItem { Name = "Майка (тонкая футболка)", Term = 255 },
                    new CommonSuggestionItem { Name = "Колготки", Term = 127 },
                    new CommonSuggestionItem { Name = "Водолазка (хлопок)", Term = 4 },
                    new CommonSuggestionItem { Name = "Водолазка", Term = 368 },
                    new CommonSuggestionItem { Name = "Шерстяная или флисовая поддева", Term = 3 },
                    new CommonSuggestionItem { Name = "Комбинезон (куртка и штаны удобнее, чем слитный вариант) и варежки (лучше непромокаемые)", Term = 63 },
                    new CommonSuggestionItem { Name = "Тонкая шапочка", Term = 248 },
                    new CommonSuggestionItem { Name = "Плотная шапочка (шлем)", Term = 7 },
                    new CommonSuggestionItem { Name = "Валенки или мембранные ботинки (для активного движения, отводят пот от тела)", Term = 63 },
                    new CommonSuggestionItem { Name = "Толстовка (хлопок, флис) с капюшоном или дождевик (непромокаемый костюм)", Term = 192 },
                    new CommonSuggestionItem { Name = "Брюки (хлопок, флис, вельвет) на мягкой резинке или джинсы", Term = 448 },
                    new CommonSuggestionItem { Name = "Мембранные ботинки (для активного движения, отводят пот от тела)", Term = 112 },
                    new CommonSuggestionItem { Name = "Легкие ботинки или резиновые сапоги на случай дождя", Term = 384 },
                    new CommonSuggestionItem { Name = "Памперс", Term = 768 },
                    new CommonSuggestionItem { Name = "Трусики х/б", Term = 7168 },
                    new CommonSuggestionItem { Name = "Панамка от солнца", Term = 7680 },
                    new CommonSuggestionItem { Name = "Толстовка, футболка с длинным или коротким рукавом, штаны, носки", Term = 256 },
                    new CommonSuggestionItem { Name = "Песочник/ футболка и шорты/юбка или сарафан/платье", Term = 7680 },
                    new CommonSuggestionItem { Name = "Ботинки/сандалии", Term = 7936 },
                    new CommonSuggestionItem { Name = "Худи", Term = 7936 }
                }
            },
            { CommonSuggestionType.Child1824, new List<CommonSuggestionItem>
                {
                    new CommonSuggestionItem { Name = "Памперс (или трусики х/б)", Term = 1023 },
                    new CommonSuggestionItem { Name = "Майка (футболка с синтетикой)", Term = 255 },
                    new CommonSuggestionItem { Name = "Колготки (синтетика)", Term = 63 },
                    new CommonSuggestionItem { Name = "Водолазка (синтетика)", Term = 8 },
                    new CommonSuggestionItem { Name = "Флисовая спецподдева", Term = 7 },
                    new CommonSuggestionItem { Name = "Комбинезон (мембрана) варежки (лучше непромокаемые)", Term = 63 },
                    new CommonSuggestionItem { Name = "Тонкая шапочка", Term = 248 },
                    new CommonSuggestionItem { Name = "Плотная шапочка (шлем)", Term = 7 },
                    new CommonSuggestionItem { Name = "Мембранные ботинки", Term = 127 },
                    new CommonSuggestionItem { Name = "Колготки", Term = 127 },
                    new CommonSuggestionItem { Name = "Водолазка", Term = 368 },
                    new CommonSuggestionItem { Name = "Толстовка (хлопок, флис) с капюшоном или дождевик (непромокаемый костюм)", Term = 192 },
                    new CommonSuggestionItem { Name = "Брюки (хлопок, флис) на мягкой резинке или джинсы", Term = 448 },
                    new CommonSuggestionItem { Name = "Легкие ботинки или резиновые сапоги на случай дождя", Term = 384 },
                    new CommonSuggestionItem { Name = "Памперс", Term = 768 },
                    new CommonSuggestionItem { Name = "Трусики х/б", Term = 7168 },
                    new CommonSuggestionItem { Name = "Панамка от солнца", Term = 7680 },
                    new CommonSuggestionItem { Name = "Толстовка, футболка с длинным или коротким рукавом, штаны, носки", Term = 256 },
                    new CommonSuggestionItem { Name = "Футболка и шорты/юбка или сарафан/платье", Term = 7680 },
                    new CommonSuggestionItem { Name = "Ботинки/сандалии", Term = 7936 },
                    new CommonSuggestionItem { Name = "Худи", Term = 7936 }
                }
            }
        };

        private readonly Dictionary<CommonSuggestionType, List<CommonSuggestionItem>> commonItemsENG = new Dictionary<CommonSuggestionType, List<CommonSuggestionItem>>
        {
            { CommonSuggestionType.Child03, new List<CommonSuggestionItem>
                {
                    new CommonSuggestionItem { Name = "Pampers", Term = 8191 },
                    new CommonSuggestionItem { Name = "Slip (cotton jumpsuit with hands and feet closed) or body with long sleeves and pants with socks (cotton or flannel)", Term = 508 },
                    new CommonSuggestionItem { Name = "Wool or flannel jumpsuit", Term = 12 },
                    new CommonSuggestionItem { Name = "Fleece envelope or jumpsuit", Term = 56 },
                    new CommonSuggestionItem { Name = "Thin cap", Term = 496 },
                    new CommonSuggestionItem { Name = "Thick cap", Term = 12 },
                    new CommonSuggestionItem { Name = "Envelope jumpsuit on a sheepskin", Term = 4 },
                    new CommonSuggestionItem { Name = "Blanket fleece or flannel", Term = 508 },
                    new CommonSuggestionItem { Name = "Cotton envelope or bag", Term = 192 },
                    new CommonSuggestionItem { Name = "Disposable diapers", Term = 6144 },
                    new CommonSuggestionItem { Name = "Panama from the sun (if the child walks in a sling or hands)", Term = 7680 },
                    new CommonSuggestionItem { Name = "Thin hat / cap", Term = 256 },
                    new CommonSuggestionItem { Name = "Body / T-shirt / vest or sundress", Term = 3584 },
                    new CommonSuggestionItem { Name = "Easy blanket or diaper", Term = 7936 }
                }
            },
            { CommonSuggestionType.Child36, new List<CommonSuggestionItem>
                {
                    new CommonSuggestionItem { Name = "Pampers", Term = 8191 },
                    new CommonSuggestionItem { Name = "Slip (cotton jumpsuit with hands and feet closed) or body with long sleeves and pants with socks (cotton or flannel)", Term = 380 },
                    new CommonSuggestionItem { Name = "Wool hooking", Term = 12 },
                    new CommonSuggestionItem { Name = "Fleece envelope or jumpsuit", Term = 56 },
                    new CommonSuggestionItem { Name = "Thin cap", Term = 248 },
                    new CommonSuggestionItem { Name = "Thick cap", Term = 12 },
                    new CommonSuggestionItem { Name = "Envelope-suit on a sheepskin or fluff + mittens and boots on the toe of wool", Term = 6 },
                    new CommonSuggestionItem { Name = "Blanket fleece or flannel", Term = 508 },
                    new CommonSuggestionItem { Name = "Body with long sleeves", Term = 384 },
                    new CommonSuggestionItem { Name = "Turtleneck (jersey)", Term = 128 },
                    new CommonSuggestionItem { Name = "Sweatshirt (cotton, fleece) or blouse with a hood", Term = 448 },
                    new CommonSuggestionItem { Name = "Trousers (cotton, fleece, corduroy) on a soft elastic or jeans with socks", Term = 448 },
                    new CommonSuggestionItem { Name = "Shorts / cotton diaper", Term = 6144 },
                    new CommonSuggestionItem { Name = "Panama from the sun (if the child walks in a sling or hands)", Term = 7680 },
                    new CommonSuggestionItem { Name = "Thin hat / cap", Term = 256 },
                    new CommonSuggestionItem { Name = "Body / T-shirt / vest or sundress", Term = 3584 },
                    new CommonSuggestionItem { Name = "Easy blanket or diaper", Term = 7936 }
                }
            },
            { CommonSuggestionType.Child69, new List<CommonSuggestionItem>
                {
                    new CommonSuggestionItem { Name = "Pampers", Term = 2047 },
                    new CommonSuggestionItem { Name = "Body with sleeves", Term = 255 },
                    new CommonSuggestionItem { Name = "Tights", Term = 127 },
                    new CommonSuggestionItem { Name = "Wool or fleece hooking", Term = 7 },
                    new CommonSuggestionItem { Name = "Overalls and mittens", Term = 63 },
                    new CommonSuggestionItem { Name = "Thin cap", Term = 248 },
                    new CommonSuggestionItem { Name = "Thick cap (hat)", Term = 7 },
                    new CommonSuggestionItem { Name = "Socks and boots or shoes with sheepskin, if there is no fur on pinetok suit", Term = 63 },
                    new CommonSuggestionItem { Name = "Blanket fleece or flannel", Term = 511 },
                    new CommonSuggestionItem { Name = "Turtleneck (jersey)", Term = 192 },
                    new CommonSuggestionItem { Name = "Sweatshirt (cotton, fleece) or waterproof jacket with a hood in case of rain", Term = 448 },
                    new CommonSuggestionItem { Name = "Trousers (cotton, fleece, corduroy) on a soft elastic or jeans", Term = 448 },
                    new CommonSuggestionItem { Name = "Wool socks or booties Warm (shoes)", Term = 240 },
                    new CommonSuggestionItem { Name = "Light shoes or socks", Term = 256 },
                    new CommonSuggestionItem { Name = "Shorts / cotton diaper", Term = 6144 },
                    new CommonSuggestionItem { Name = "Slip (cotton jumpsuit with hands and feet closed) or body with long sleeves and pants with socks (cotton or flannel)", Term = 256 },
                    new CommonSuggestionItem { Name = "Panama from the sun (if the child walks in a sling or hands)", Term = 7680 },
                    new CommonSuggestionItem { Name = "Thin hat / cap", Term = 256 },
                    new CommonSuggestionItem { Name = "Body / T-shirt / vest or sundress", Term = 3584 },
                    new CommonSuggestionItem { Name = "Easy blanket or diaper", Term = 7936 }
                }
            },
            { CommonSuggestionType.Child912, new List<CommonSuggestionItem>
                {
                    new CommonSuggestionItem { Name = "Pampers", Term = 1023 },
					new CommonSuggestionItem { Name = "Сotton panties", Term = 7168 },
                    new CommonSuggestionItem { Name = "Mike (thin T-shirt or tank tops with sleeves", Term = 511 },
                    new CommonSuggestionItem { Name = "Tights", Term = 127 },
                    new CommonSuggestionItem { Name = "Turtleneck (cotton)", Term = 4 },
                    new CommonSuggestionItem { Name = "Wool or fleece hooking", Term = 7 },
                    new CommonSuggestionItem { Name = "Overalls and gloves (preferably waterproof)", Term = 63 },
                    new CommonSuggestionItem { Name = "Thin cap", Term = 248 },
                    new CommonSuggestionItem { Name = "Thick cap (hat)", Term = 7 },
                    new CommonSuggestionItem { Name = "Boots or waterproof shoes", Term = 63 },
                    new CommonSuggestionItem { Name = "Turtleneck (jersey)", Term = 192 },
                    new CommonSuggestionItem { Name = "Sweatshirt (cotton, fleece) or waterproof jacket with a hood in case of rain", Term = 448 },
                    new CommonSuggestionItem { Name = "Trousers (cotton, fleece, corduroy) on a soft elastic or jeans", Term = 448 },
                    new CommonSuggestionItem { Name = "Warm waterproof boots", Term = 112 },
                    new CommonSuggestionItem { Name = "Light shoes or rubber boots in case of rain", Term = 384 },
                    new CommonSuggestionItem { Name = "Panama from the sun", Term = 7680 },
                    new CommonSuggestionItem { Name = "Sweatshirt T-shirt with long or short sleeves, pants, socks", Term = 256 },
                    new CommonSuggestionItem { Name = "Sandman / T-shirt and shorts / skirt or sundress / dress", Term = 7680 },
                    new CommonSuggestionItem { Name = "Shoes / sandals (in a wheelchair does not need)", Term = 7936 },
                    new CommonSuggestionItem { Name = "Hoodie", Term = 7936 }
                }
            },
            { CommonSuggestionType.Child1218, new List<CommonSuggestionItem>
                {
                    new CommonSuggestionItem { Name = "Pampers (or cotton panties)", Term = 1023 },
					new CommonSuggestionItem { Name = "Mike (thin T-shirt)", Term = 255 },
                    new CommonSuggestionItem { Name = "Tights", Term = 127 },
                    new CommonSuggestionItem { Name = "Turtleneck (cotton)", Term = 4 },
                    new CommonSuggestionItem { Name = "Turtleneck", Term = 368 },
                    new CommonSuggestionItem { Name = "Wool or fleece hooking", Term = 3 },
                    new CommonSuggestionItem { Name = "Overalls (jacket and pants more comfortable than fused version) and gloves (preferably waterproof)", Term = 63 },
                    new CommonSuggestionItem { Name = "Thin cap", Term = 248 },
                    new CommonSuggestionItem { Name = "Thick cap (hat)", Term = 7 },
                    new CommonSuggestionItem { Name = "Boots or shoes membrane (for active motion is withdrawn sweat away from the body)", Term = 63 },
                    new CommonSuggestionItem { Name = "Sweatshirt (cotton, fleece) or a raincoat with a hood (oilskin)", Term = 192 },
                    new CommonSuggestionItem { Name = "Trousers (cotton, fleece, corduroy) on a soft elastic or jeans", Term = 448 },
                    new CommonSuggestionItem { Name = "Membrane shoes (for the active motion is withdrawn sweat away from the body)", Term = 112 },
                    new CommonSuggestionItem { Name = "Light shoes or rubber boots in case of rain", Term = 384 },
                    new CommonSuggestionItem { Name = "Pampers", Term = 768 },
                    new CommonSuggestionItem { Name = "Cotton panties", Term = 7168 },
                    new CommonSuggestionItem { Name = "Panama from the sun", Term = 7680 },
                    new CommonSuggestionItem { Name = "Sweatshirt T-shirt with long or short sleeves, pants, socks", Term = 256 },
                    new CommonSuggestionItem { Name = "Sandman / T-shirt and shorts / skirt or sundress / dress", Term = 7680 },
                    new CommonSuggestionItem { Name = "Shoes / sandals", Term = 7936 },
                    new CommonSuggestionItem { Name = "Hoodie", Term = 7936 }
                }
            },
            { CommonSuggestionType.Child1824, new List<CommonSuggestionItem>
                {
                    new CommonSuggestionItem { Name = "Pampers (or cotton panties)", Term = 1023 },
					new CommonSuggestionItem { Name = "Mike (T-shirt with synthetics)", Term = 255 },
                    new CommonSuggestionItem { Name = "Tights (synthetics)", Term = 63 },
                    new CommonSuggestionItem { Name = "Turtleneck (synthetics)", Term = 8 },
                    new CommonSuggestionItem { Name = "Fleece spetspoddeva", Term = 7 },
                    new CommonSuggestionItem { Name = "Overalls (membrane) gloves (preferably waterproof)", Term = 63 },
                    new CommonSuggestionItem { Name = "Thin cap", Term = 248 },
                    new CommonSuggestionItem { Name = "Thick cap (hat)", Term = 7 },
                    new CommonSuggestionItem { Name = "Membrane boots", Term = 127 },
                    new CommonSuggestionItem { Name = "Tights", Term = 127 },
                    new CommonSuggestionItem { Name = "Turtleneck", Term = 368 },
                    new CommonSuggestionItem { Name = "Sweatshirt (cotton, fleece) or a raincoat with a hood (oilskin)", Term = 192 },
                    new CommonSuggestionItem { Name = "Trousers (cotton, fleece) on a soft elastic or jeans", Term = 448 },
                    new CommonSuggestionItem { Name = "Light shoes or rubber boots in case of rain", Term = 384 },
                    new CommonSuggestionItem { Name = "Pampers", Term = 768 },
                    new CommonSuggestionItem { Name = "Panties x / w", Term = 7168 },
                    new CommonSuggestionItem { Name = "Panama from the sun", Term = 7680 },
                    new CommonSuggestionItem { Name = "Sweatshirt T-shirt with long or short sleeves, pants, socks", Term = 256 },
                    new CommonSuggestionItem { Name = "T-shirt and shorts / skirt or sundress / dress", Term = 7680 },
                    new CommonSuggestionItem { Name = "Shoes / sandals", Term = 7936 },
                    new CommonSuggestionItem { Name = "Hoodie", Term = 7936 }
                }
            }
        };

        #endregion

        #region Private fields

        private readonly IDataStore _dataStore;
        private readonly IUserProfileStore _userProfileStore;

        #endregion

        #region Constructos

        /// <summary>
        /// Initializes a new instance of the <see cref="SuggestionService"/> class.
        /// </summary>
        /// <param name="dataStore">The data store.</param>
        /// <param name="userProfileStore">The user profile store.</param>
        public SuggestionService(IDataStore dataStore, IUserProfileStore userProfileStore)
        {
            this._dataStore = dataStore;
            this._userProfileStore = userProfileStore;
        }
        
        #endregion

        #region Public methods

        /// <summary>
        /// Gets the suggestion.
        /// </summary>
        /// <param name="woeid">The woeid.</param>
        /// <param name="languageId">The language identifier.</param>
        /// <returns></returns>
        public SuggestionsResult GetSuggestion(string woeid, int languageId, string userId)
        {
            var location = this._dataStore.GetLocationByWOEID(woeid);

            if (location != null)
            {
                var condition = this._dataStore.GetLastWeatherConditionByLocation(location.Id);

                if (condition != null)
                {
                    return this._GetSuggestion(condition, languageId, userId);
                }
            }

            return null;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Gets the suggestion.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <returns></returns>
        private SuggestionsResult _GetSuggestion(WeatherCondition condition, int languageId, string userId)
        {
            var result = new SuggestionsResult();

            List<int> localItemProvidersIds = new List<int>();

            if (!string.IsNullOrEmpty(userId))
            {
                var userProfile = this._userProfileStore.GetUserProfileById(userId);

                if (userProfile == null)
                {
                    this._userProfileStore.Insert(new UserProfile
                    {
                        PhiUserId = userId,
                        NotifyMeAboutSuddenWeatherEvents = true // True by default
                    });
                }
            }
            
            string forecastDescription = null;

            var availableItems = this._dataStore.GetAllAvailableItemIds();
            var actionTypes = this._dataStore.GetAllActiveActionTypes(languageId);
            var factors = this._dataStore.GetAllFactors();
            var factorTypes = this._dataStore.GetAllFactorTypes();
            var allRules = this._dataStore.GetAllRules();

            foreach (ActionType actionType in actionTypes)
            {
                List<Item> itemsToSuggest = new List<Item>();

                string actionName = actionType.Name;

                int termsSum = _GetTermsSum(factors, factorTypes, allRules, actionType, ConvertConditionsForUnits(condition, languageId));
                if (termsSum > 0)
                {
                    var items = this._dataStore
                        .GetItemsBySuggestionTerm(termsSum, languageId)
                        .OrderBy(x => x.SuggestionTerms.Value)
                        //.ThenBy(x => x.DefaultImageUri)
                        .ThenByDescending(x => x.Created)
                        .Where(x => (!x.IsPublic.HasValue || x.IsPublic.Value) &&
                                    (!x.AvailableTill.HasValue || !condition.ForecastDate.HasValue || x.AvailableTill.Value > condition.ForecastDate) &&
                                    availableItems.Contains(x.Id))
                        .ToList();

                    // TODO User items should be taken only by weather conditions?
                    //IEnumerable<Item> userItems = null;
                    //if (!string.IsNullOrEmpty(userId))
                    //{
                    //    userItems = this._dataStore
                    //        .GetItemProvidersByUserProfile(userId)
                    //        .Select(x => this._dataStore.GetItemProviderById(x.ItemProviderId.Value))
                    //        .SelectMany(x => this._dataStore.GetItemsByProviderId(x.Id))
                    //        .Where(x => x.ActionTypeId == actionType.Id);
                    //}

                    // In case of 'Walking' add umbrella's and caps, glasses and hats, etc.
                    if (actionType.Id == 14 || actionType.Id == 13)
                    {
                        itemsToSuggest.AddRange(items.Where(x => x.ActionTypeId == 2 || x.ActionTypeId == 1)); // 1, 2 - Neutral (umbrella, cap, hat, glasses).
                    }

                    // Add user items first.
                    //if (userItems != null && userItems.Any())
                    //{
                    //    itemsToSuggest.AddRange(userItems);
                    //}

                    itemsToSuggest.AddRange(
                        items
                        .Where(x => x.ActionTypeId == actionType.Id)
                        .OrderBy(x => x.SuggestionTerms.Value)
                        .ThenBy(x => x.ShowedTimes));

                    // Now let's calculate common suggestions for children.
                    if (actionType.Id == 11 || actionType.Id == 12)
                    {
                        if (languageId == 1) // English
                        {
                            foreach (var key in commonItemsENG.Keys)
                            {
                                result.CommonSuggestions.Add(key.ToString().ToLower(),
                                    commonItemsENG[key].Where(x => (x.Term & termsSum) > 0).Select(x => x.Name).ToList());
                            }
                        }
                        else // Russian
                        {
                            foreach (var key in commonItemsRUS.Keys)
                            {
                                result.CommonSuggestions.Add(key.ToString().ToLower(),
                                    commonItemsRUS[key].Where(x => (x.Term & termsSum) > 0).Select(x => x.Name).ToList());
                            }
                        }
                    }
                }

                if (string.IsNullOrEmpty(forecastDescription))
                {
                    var termNamesForNotes = this._dataStore.GetSuggestionTermBySumValue(termsSum, languageId);
                    int count = termNamesForNotes.Count();

                    // Осторожно, говнокод.
                    if (count > 3)
                    {
                        // Нездоровая хрень, тут будут термины от Холодновато до Снимай трусы. Показываем самый теплый прогноз (чтобы не одеть человека в шубу летом),
                        // так как все более "холодные прогнозы" - это уже наша паранойя.
                        var maxTerm = termNamesForNotes.OrderBy(x => x.Value.Value).LastOrDefault();
                        forecastDescription = maxTerm.Name;
                    }
                    else
                    {
                        for (int i = 0; i < termNamesForNotes.Count(); i++)
                        {
                            forecastDescription += termNamesForNotes[i].Name + ". ";
                        }
                    }
                }
                
                if (itemsToSuggest.Any())
                {
                    var suggestion = new Suggestion
                    {
                        Created = DateTime.UtcNow,
                        WeatherConditionId = condition.Id,
                        ShortDescription = String.Empty,
                        FullDescription = forecastDescription
                    };

                    // Get each type only once...and get it in Descending order by Suggestion Term.
                    foreach (Item itemToSuggest in itemsToSuggest)
                    {
                        suggestion.SuggestionItems.Add(
                            new SuggestionItem { Item = itemToSuggest, ItemId = itemToSuggest.Id, Suggestion = suggestion });
                    }

                    result.Suggestions.Add(actionName, suggestion);
                }
            }

            return result;
        }

        private int _GetTermsSum(IList<Factor> factors, IList<FactorType> factorTypes, IList<Rule> allRules, ActionType actionType, WeatherCondition condition)
        {
            HashSet<Int32> terms = new HashSet<int>();

            // Calculate sum of rules for factors which have additional influence: EffectiveTemperature.
            var filteredFactors = factors.Where(model => model.ActionTypeId == actionType.Id);
            foreach (var factor in filteredFactors)
            {
                var factorType = factorTypes.FirstOrDefault(model => model.Id == factor.FactorTypeId.Value);

                if (factorType.Name == FactorTypes.EffectiveTemperature.ToString())
                {
                    // Calculate effective temperature (or just get it) and assign factor Value to it.
                    Double? realEffectiveTemperature = this._GetEffectiveTemperatureWithAdditionalTemperatureFactor(condition, factor.Value.Value);

                    if (realEffectiveTemperature.HasValue)
                    {
                        terms.Add(this._GetTermsSumFromEffectiveTemperatureRules(allRules, factor.FactorTypeId.Value,
                            realEffectiveTemperature.Value, condition.TemperatureMax.Value, condition.TemperatureMin.Value));
                    }
                }
                else
                {
                    Debug.Assert(false);
                }
            }

            // Calculate all other factors.
            var windSpeedFactor = factorTypes.FirstOrDefault(model => model.Name == "WindSpeed");
            if (windSpeedFactor != null)
            {
                terms.Add(this._GetTermsSumFromRules(allRules, windSpeedFactor.Id, condition.WindSpeed == null ? 0 : condition.WindSpeed.Value));
            }

            // We don't want to suggest to take umbrella in Winter :).
            if ((!condition.TemperatureMax.HasValue || condition.TemperatureMax.Value > 0) && (!condition.TemperatureMin.HasValue || condition.TemperatureMin.Value > 0))
            {
                var atmosphereHumidityFactor = factorTypes.FirstOrDefault(model => model.Name == "AtmosphereHumidity");
                if (atmosphereHumidityFactor != null)
                {
                    terms.Add(this._GetTermsSumFromRules(allRules, atmosphereHumidityFactor.Id, condition.AthmosphereHumidity == null ? 0 : condition.AthmosphereHumidity.Value));
                }

                // TODO Maybe use this factor for snow or rain protection instead of humidity?
                var precipitationFactor = factorTypes.FirstOrDefault(x => x.Name == "Precipitation");
                if (precipitationFactor != null)
                {
                    terms.Add(this._GetTermsSumFromRules(allRules, precipitationFactor.Id, condition.Precipitation));
                }
            }

            var conditionFactor = factorTypes.FirstOrDefault(model => model.Name == "Condition");
            if (conditionFactor != null)
            {
                terms.Add(this._GetTermsSumFromRules(allRules, conditionFactor.Id, condition.Condition == null ? 0 : condition.Condition.Value));
            }

            return terms.Sum();
        }

        private WeatherCondition ConvertConditionsForUnits(WeatherCondition condition, int languageId)
        {
            WeatherCondition result = condition;

            // Calculations shoulld be made in Celsius (as deisgned), so we need to convert Fahrenheits to Celsius.
            if (languageId == 1) // English => Fahrenheits
            {
                if (condition.Temperature.HasValue)
                {
                    result.Temperature = (condition.Temperature.Value - 32) * 5 / 9;
                }

                if (condition.TemperatureMin.HasValue)
                {
                    result.TemperatureMin = (condition.TemperatureMin.Value - 32) * 5 / 9;
                }

                if (condition.TemperatureMax.HasValue)
                {
                    result.TemperatureMax = (condition.TemperatureMax.Value - 32) * 5 / 9;
                }

                if (condition.EffectiveTemperature.HasValue)
                {
                    result.EffectiveTemperature = (condition.EffectiveTemperature.Value - 32) * 5 / 9;
                }
            }

            return result;
        }

        /// <summary>
        /// Calculates the effective temperature.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="additionalTemperature">The additional temperature.</param>
        /// <returns></returns>
        private Double? _GetEffectiveTemperatureWithAdditionalTemperatureFactor(WeatherCondition condition, Double additionalTemperature)
        {
            Double? effectiveTemperature = null;
            if (!condition.IsPrecalculatedEffectiveTemperature)
            {
                if (condition.Temperature.HasValue)
                {
                    effectiveTemperature = condition.Temperature.Value;
                }
                else if (condition.TemperatureMin.HasValue && condition.TemperatureMax.HasValue)
                {
                    // Not actually effective, but middle.
                    effectiveTemperature = (condition.TemperatureMax + condition.TemperatureMin) / 2;
                }
            }
            else
            {
                effectiveTemperature = condition.EffectiveTemperature;
            }

            return effectiveTemperature;
        }

        /// <summary>
        /// Gets terms from rules for effective temperature.
        /// </summary>
        /// <param name="rules">The rules.</param>
        /// <param name="factorTypeId">The factor type identifier.</param>
        /// <param name="value">The value.</param>
        /// <param name="maxValue">The max value.</param>
        /// <param name="minValue">The min value.</param>
        /// <returns></returns>
        private int _GetTermsSumFromEffectiveTemperatureRules(IEnumerable<Rule> rules, int factorTypeId, Double value, Double maxValue, Double minValue)
        {
            var acceptRules = rules.Where(x => x.FactorTypeId.Value == factorTypeId &&
                                               (x.MinValue.HasValue && x.MaxValue.HasValue && x.MinValue.Value <= value && value <= x.MaxValue) &&
                                               x.Result.HasValue);

            var toSum = acceptRules.Select(y => y.Result.Value).Distinct();

            return toSum.Sum();
        }

        /// <summary>
        /// Gets terms from rules.
        /// </summary>
        /// <param name="rules">The rules.</param>
        /// <param name="factorTypeId">The factor type identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private int _GetTermsSumFromRules(IEnumerable<Rule> rules, int factorTypeId, Double value)
        {
            var acceptRules = rules.Where(x => x.FactorTypeId.Value == factorTypeId &&
                                   ((!x.Value.HasValue && x.MinValue.HasValue && x.MaxValue.HasValue && x.MinValue.Value <= value && value <= x.MaxValue) ||
                                    (x.Value.HasValue && x.Value == value && !x.MinValue.HasValue && !x.MaxValue.HasValue)) &&
                                    x.Result.HasValue);

            var toSum = acceptRules.Select(y => y.Result.Value).Distinct();

            return toSum.Sum();
        }

        /// <summary>
        /// Gets terms from rules.
        /// </summary>
        /// <param name="rules">The rules.</param>
        /// <param name="factorTypeId">The factor type identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private int _GetTermsSumFromRules(IEnumerable<Rule> rules, int factorTypeId, int value)
        {
            var acceptRules = rules.Where(x => x.FactorTypeId.Value == factorTypeId &&
                                   ((!x.Value.HasValue && x.MinValue.HasValue && x.MaxValue.HasValue && x.MinValue.Value <= value && value <= x.MaxValue) ||
                                    (x.Value.HasValue && x.Value == value && !x.MinValue.HasValue && !x.MaxValue.HasValue)) &&
                                    x.Result.HasValue);

            var toSum = acceptRules.Select(y => y.Result.Value).Distinct();

            return toSum.Sum();
        }

        /// <summary>
        /// Gets index of nearest.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private int _GetIndexOfNearest(Double[] array, Double value)
        {
            int ii = -1;
            for (Int32 i = 0; i < array.Length; i++)
            {
                if (Math.Abs(value - array[i]) < TOLERANCE)
                {
                    ii = i;
                    break;
                }

                if (value < array[i])
                {
                    Debug.Assert(i > 0);

                    if (i > 0)
                    {
                        if (array[i] - value > array[i - 1] - value)
                        {
                            ii = i - 1;
                        }
                        else
                        {
                            ii = i;
                        }
                    }
                }
            }

            return ii;
        }

        #endregion
    }
}
