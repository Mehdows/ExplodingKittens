using Newtonsoft.Json;
using System.Collections;
namespace explodingKittens
{
        class json{
        
        public static String readListJson(String fileName){
            String fileContent = "";
            try{
                var path = Path.Combine(Directory.GetCurrentDirectory(), @"json\\" +fileName);
                fileContent = System.IO.File.ReadAllText(path);
            }
            catch(Exception e){
                throw new Exception("FILE_NOT_FOUND_EXCEPTION", e);
            }
            return fileContent;
            
        }

        public static ArrayList jsonToList(String json){
            ArrayList lst = new ArrayList();
            try{
                JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                JsonConvert.PopulateObject(json, lst, settings);
            }
            catch(Exception e){
                throw new Exception("JSON_TO_LIST_EXCEPTION", e);
            }
            return lst;
        }

        public static String listToJson(ArrayList lst){
            try{
                JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, Formatting = Formatting.Indented };
                return JsonConvert.SerializeObject(lst, settings);
            }
            catch(Exception e){
                throw new Exception("LIST_TO_JSON_EXCEPTION", e);
            }
        }
    }

}