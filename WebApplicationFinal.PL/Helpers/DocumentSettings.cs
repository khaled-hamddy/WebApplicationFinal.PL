namespace WebApplicationFinal.PL.Helpers
{
    public class DocumentSettings
    {
        //Method will UPLoad file,
        //return string because it return path that will put it at Database
        public static string UploadFile(IFormFile file, string folderName)
        {
            //What are steps to Implement UploadFile method ?

            //1.Get Located Folder Path 
            //string folderPath = "E:\\Asp.Net\\MVC\\07 ASP.NET Core MVC\\Session 01\\Demos\\Day03 Demo Solution\\Demo.PL\\wwwroot\\files\\" + folderName;
            //Return path folder that contain this project(with Dynamic way) Depending on it deployed on  which server 
            //string folderPath = Directory.GetCurrentDirectory() + @"\wwwroot\files\"+ folderName;
            //There Method named (Path.Combine) that Combine folderPath without using to plus operator
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);

            //Get File Name and Make it UNIQUE (Guid) 
            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            //3. Get File Path =[Folder Path + File Name ]
            string filePath = Path.Combine(folderPath, fileName);

            //4. Save File as Streams[Data per Time]

            //using To dispose File
            using var fileStream = new FileStream(filePath, FileMode.Create);

             file.CopyTo(fileStream);

            return fileName; //because file path will be duplication
        }

        public static void DeleteFile(string fileName, string folderName)
        {

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName, fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);

        }
    }
}
