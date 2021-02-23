namespace web_04_ef_sqlite.Models.Database{
    public class Student{
        public int Id {get; set;}
        public string Name {get; set;}

        public int Age {get;set;}

        //ManyToOne -> a Student belongs to one Location, one Location can be linked to many Students
        public Location Location {get;set;}

    }
}