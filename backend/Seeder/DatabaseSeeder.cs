
using Core.Domain;
using Core.Interfaces;
using System;
using System.IO;
using System.Linq;


namespace Seeder
{
    public class DatabaseSeeder
    {
        private readonly IUnitOfWork _unitOfWork;

        public DatabaseSeeder(
            IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public void Seed()
        {        
            if (!_unitOfWork.Interests.Get().Any())
            {
                InsertInterests();
            }   
        }

        private void InsertInterests()
        {
            string line;
            int insertedCount = 0;
            var basePath = Directory.GetCurrentDirectory();
            basePath = Directory.GetParent(basePath).FullName;
            basePath = Directory.GetParent(basePath).FullName;
            basePath = Directory.GetParent(basePath).FullName;
            basePath = Path.Combine(basePath, "interests.txt");


            System.IO.StreamReader file = new System.IO.StreamReader(basePath);

            var interest = new Interest() { ThumbnailImgUrl = "" };

            while ((line = file.ReadLine()) != null)
            {
                interest.Id = Guid.NewGuid();
                interest.Name = line;
                _unitOfWork.Interests.Add(interest);
                insertedCount++;

                if (insertedCount % 100000 == 0)
                {
                    Console.WriteLine("Wrote {0} interests in db", insertedCount);
                    
                }
                _unitOfWork.Complete();
            }

            file.Close();
        }
    }
}
