namespace explodingKittens{

    public class CardPackFactory{
        public static CardPack createCardPack(String type){
            switch(type){
                case "Hand":
                    return new Hand();
                default:
                    return new ExplodingKittens();
            }
        }
    }



}