using Dhrutara.WriteWise.App.Providers.Constants;

namespace Dhrutara.WriteWise.App.Providers.LocalStorage
{
    public class LocalContent
    {
        private record Content(ContentType ContentType, ContentCategory Category, Relationship Relationship, string Text);

        private static List<Content> ContentList => new()
        {
            new Content(ContentType.Message, ContentCategory.Birthday,Relationship.Wife, "Happy Birthday to my beautiful wife! On this special day, I want to thank you for all you do and all you are. You bring love and joy to my life, and I'm so grateful that you are my partner. I'm looking forward to all the wonderful things we will do and experiences we will share in the coming year. Wishing you a wonderful day and an amazing year ahead. I love you!"),
            new Content(ContentType.Message, ContentCategory.Birthday,Relationship.Wife, "Happy birthday to my beautiful wife! Today is all about you and I want you to know how much I love and appreciate you. You bring joy to my life and I'm so lucky to have you in it. Wishing you a day full of love and happiness, now and always."),
            new Content(ContentType.Message, ContentCategory.Birthday,Relationship.Husband, "Happy Birthday to the love of my life! I hope this day is filled with love, joy, and all the things that make you smile. I'm so lucky to have such a wonderful husband. You've been my rock, my confidant, and my best friend. I love you with all my heart and I can't wait to celebrate many more birthdays with you!"),
            new Content(ContentType.Message, ContentCategory.Birthday,Relationship.Husband, "Happy Birthday, my love! You are the most amazing and special person in my life. The day you were born was the best day of my life.  Thanks for being there for me and for all your unconditional love and support. I hope your birthday is filled with lots of love and joy! Wishing you all the best, lots of love and hugs!"),
            new Content(ContentType.Message, ContentCategory.Birthday,Relationship.Mother, "Happy birthday to the most amazing mother in the world! You have been my rock, my confidant, and my best friend. You have shown me what it means to be strong, kind, and compassionate. You have given me so much love and support throughout my life, and I am forever thankful for that. Wishing you a day full of joy and happiness, love and laughter!"),
            new Content(ContentType.Message, ContentCategory.Birthday,Relationship.Mother, "Happy Birthday, Mom! You are an incredible woman who has been such an amazing role model for me. You have taught me so much about life and have always been there for me whenever I needed you. You have a heart of gold and I am so blessed to call you my mother. Wishing you a day filled with love and joy!"),
            new Content(ContentType.Message, ContentCategory.Birthday,Relationship.Son, "Happy Birthday to my wonderful son! You are an amazing person who deserves all the love and happiness in the world. May your special day be filled with joy and may all your wishes come true. Wishing you a very happy birthday!"),
            new Content(ContentType.Message, ContentCategory.Birthday,Relationship.Son, "Happy birthday, my handsome and amazing son! You're the light of our lives, and we love you so much! You are so special and so loved, and we are so grateful to have you in our lives! We are so excited to see all the wonderful things you will accomplish in the next year! We love you so much!"),
            new Content(ContentType.Message, ContentCategory.Birthday,Relationship.Brother, "Happy birthday, my brother! I love you so much and I am grateful for all the amazing moments we've shared together. You are my best friend and I can't wait to see what the future holds for us."),
            new Content(ContentType.Message, ContentCategory.Birthday,Relationship.Brother, "Happy birthday, my brother! You are amazing and I love you so much! You are such a fun and loving person and I know that you will continue to be an amazing person. I hope that you have a wonderful birthday and that all of your dreams come true!"),
            new Content(ContentType.Message, ContentCategory.Birthday,Relationship.Sister, "Happy Birthday to my wonderful sister! You are an amazing person who deserves all the love and happiness in the world. May your special day be filled with joy and may all your wishes come true. Wishing you a very happy birthday!"),
            new Content(ContentType.Message, ContentCategory.Birthday,Relationship.Sister, "Happy birthday, my sister! You are the light of our lives, and we are so grateful for you. You make us laugh, you make us cry, and you are the best friend a person could ask for. You are the most beautiful person in the world, and we are so proud to have you as our sister."),
            new Content(ContentType.Message, ContentCategory.Birthday,Relationship.Father, "You are the best father anyone could ask for and you mean the world to me. I love you so much and I'm grateful for all the time we've spent together. You have always been there for me and I am grateful for that. I hope you have a wonderful day!"),
            new Content(ContentType.Message, ContentCategory.Birthday,Relationship.Father, "Happy birthday, Father. You have been so amazing to me and I can't imagine my life without you. You always put others before yourself and I am so grateful for that. You are truly a wonderful man and I love you so much."),
            new Content(ContentType.Message, ContentCategory.Birthday,Relationship.Friend, "Happy birthday! I hope you have a day that is as wonderful as you are. You are so special, and I enjoy spending time with you. I hope that this birthday is as wonderful as you are."),
            new Content(ContentType.Message, ContentCategory.Birthday,Relationship.Friend, "Happy Birthday to my wonderful friend! You are an amazing person who deserves all the love and happiness in the world. May your special day be filled with joy and may all your wishes come true. Wishing you a very happy birthday!"),

            new Content(ContentType.Message, ContentCategory.Romantic,Relationship.Wife, "Every day I thank the universe for sending me such an incredible soul mate like you. You are my rock, my confidant and my biggest supporter. You make my life so much brighter and I am so grateful to have you by my side. I love you with all my heart and I can't wait to spend a lifetime with you full of love and laughter."),
            new Content(ContentType.Message, ContentCategory.Romantic,Relationship.Wife, "I am so blessed and thankful to have you in my life. You bring so much love and joy to my days, and I can’t express how much I appreciate you. Every moment I spend with you is a reminder of how lucky I am, and I will cherish them forever. You are my everything and I love you more than words can express."),
            new Content(ContentType.Message, ContentCategory.Romantic,Relationship.Husband, "My love for you grows stronger with each passing day. You are my rock, my confidante, and my best friend. You bring so much joy and laughter into my life and I'm so blessed to have you in it. I want you to know that I will always be here for you and I will always love you with all of my heart. "),
            new Content(ContentType.Message, ContentCategory.Romantic,Relationship.Husband, "My love you are the most romantic person I know and I love all the special moments we share together. I'm so grateful for all the moments we have shared and all the memories we will make in the future. I love you, now and always."),
            new Content(ContentType.Message, ContentCategory.Romantic,Relationship.Girlfriend, "Every day I thank the universe for sending me such an incredible soul mate like you. You are my rock, my confidant and my biggest supporter. You make my life so much brighter and I am so grateful to have you by my side. I love you with all my heart and I can't wait to spend a lifetime with you full of love and laughter."),
            new Content(ContentType.Message, ContentCategory.Romantic,Relationship.Girlfriend, "I am so blessed and thankful to have you in my life. You bring so much love and joy to my days, and I can’t express how much I appreciate you. Every moment I spend with you is a reminder of how lucky I am, and I will cherish them forever. You are my everything and I love you more than words can express."),
            new Content(ContentType.Message, ContentCategory.Romantic,Relationship.Boyfriend, "My love for you grows stronger with each passing day. You are my rock, my confidante, and my best friend. You bring so much joy and laughter into my life and I'm so blessed to have you in it. I want you to know that I will always be here for you and I will always love you with all of my heart. "),
            new Content(ContentType.Message, ContentCategory.Romantic,Relationship.Boyfriend, "My love you are the most romantic person I know and I love all the special moments we share together. I'm so grateful for all the moments we have shared and all the memories we will make in the future. I love you, now and always."),

            new Content(ContentType.Message, ContentCategory.Condolence,Relationship.Wife, ""),
            new Content(ContentType.Message, ContentCategory.Condolence,Relationship.Wife, ""),
            new Content(ContentType.Message, ContentCategory.Condolence,Relationship.Husband, ""),
            new Content(ContentType.Message, ContentCategory.Condolence,Relationship.Husband, ""),
            new Content(ContentType.Message, ContentCategory.Condolence,Relationship.Mother, ""),
            new Content(ContentType.Message, ContentCategory.Condolence,Relationship.Mother, ""),
            new Content(ContentType.Message, ContentCategory.Condolence,Relationship.Son, ""),
            new Content(ContentType.Message, ContentCategory.Condolence,Relationship.Son, ""),
            new Content(ContentType.Message, ContentCategory.Condolence,Relationship.Brother, ""),
            new Content(ContentType.Message, ContentCategory.Condolence,Relationship.Brother, ""),
            new Content(ContentType.Message, ContentCategory.Condolence,Relationship.Sister, ""),
            new Content(ContentType.Message, ContentCategory.Condolence,Relationship.Sister, ""),
            new Content(ContentType.Message, ContentCategory.Condolence,Relationship.Father, ""),
            new Content(ContentType.Message, ContentCategory.Condolence,Relationship.Father, ""),
            new Content(ContentType.Message, ContentCategory.Condolence,Relationship.Friend, ""),
            new Content(ContentType.Message, ContentCategory.Condolence,Relationship.Friend, ""),


            new Content(ContentType.Poem, ContentCategory.Romantic,Relationship.Wife, ""),
            new Content(ContentType.Poem, ContentCategory.Romantic,Relationship.Wife, ""),
            new Content(ContentType.Poem, ContentCategory.Romantic,Relationship.Husband, ""),
            new Content(ContentType.Poem, ContentCategory.Romantic,Relationship.Husband, ""),
            new Content(ContentType.Poem, ContentCategory.Romantic,Relationship.Girlfriend, ""),
            new Content(ContentType.Poem, ContentCategory.Romantic,Relationship.Girlfriend, ""),
            new Content(ContentType.Poem, ContentCategory.Romantic,Relationship.Boyfriend, ""),
            new Content(ContentType.Poem, ContentCategory.Romantic,Relationship.Boyfriend, ""),


            new Content(ContentType.Joke, ContentCategory.Wedding,Relationship.None, ""),
            new Content(ContentType.Joke, ContentCategory.Wedding,Relationship.None, ""),
            new Content(ContentType.Joke, ContentCategory.Wedding,Relationship.None, ""),
            new Content(ContentType.Joke, ContentCategory.Wedding,Relationship.None, ""),
            new Content(ContentType.Joke, ContentCategory.WeddingAnniversary,Relationship.None, ""),
            new Content(ContentType.Joke, ContentCategory.WeddingAnniversary,Relationship.None, ""),
            new Content(ContentType.Joke, ContentCategory.WeddingAnniversary,Relationship.None, ""),
            new Content(ContentType.Joke, ContentCategory.WeddingAnniversary,Relationship.None, ""),
            new Content(ContentType.Joke, ContentCategory.WeddingAnniversary,Relationship.None, ""),
            new Content(ContentType.Joke, ContentCategory.WeddingAnniversary,Relationship.None, ""),

        };
    }


}
