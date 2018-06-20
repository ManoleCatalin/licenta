
using Core.Domain;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class InputPost
{
    public string Title { get; set; }
    public string SourceUrl { get; set; }
    public string Description { get; set; }
    public List<string> Interests{ get; set; }
}

class InputPosts
{
    public List<InputPost> Posts { get; set; }
}

namespace Seeder
{
    public class DatabaseSeeder
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbService _dbService;
        private readonly UserManager<User> _userManager;

        public DatabaseSeeder(
            IUnitOfWork unitOfWork,
            DbService dbService,
            UserManager<User> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._dbService = dbService;
            this._userManager = userManager;
        }

        public  void Seed()
        {
            _dbService.Database.EnsureCreated();


            if (!_unitOfWork.Interests.Get().Any())
            {
                InsertInterests();
            }

            if (!_unitOfWork.Users.Get().Any())
            {
                InsertUsers();
            }

            if (!_unitOfWork.Posts.Get().Any())
            {
                InsertPosts();
            }
        }

        private void InsertUsers()
        {
            var interests = _unitOfWork.Interests.Get(1, 300);
            var userInterests = new List<UserInterest>();

            _userManager.CreateAsync(new User { FirstName = "test", LastName = "test", Email = "test@test.com", UserName = "test" }, "password").Wait();
            _userManager.CreateAsync(new User { FirstName = "other", LastName = "other", Email = "other@other.com", UserName = "other" }, "password").Wait();

            var users = _unitOfWork.Users.Get(1, 2).ToList();

            foreach (var interest in interests)
            {
                _unitOfWork.UserInterests.Add(new UserInterest { InterestId = interest.Id, UserId = users[0].Id });
                _unitOfWork.UserInterests.Add(new UserInterest { InterestId = interest.Id, UserId = users[1].Id });
            }

            _unitOfWork.Complete();

        }

        private void InsertPosts()
        {
            var basePath = Directory.GetCurrentDirectory();
            basePath = Directory.GetParent(basePath).FullName;
            basePath = Directory.GetParent(basePath).FullName;
            basePath = Directory.GetParent(basePath).FullName;
            basePath = Path.Combine(basePath, "posts.txt");
            StreamReader file = new System.IO.StreamReader(basePath);
            var json = file.ReadToEnd();

            var inputPosts = JsonConvert.DeserializeObject<InputPosts>(json);
            file.Close();

            var firstUserId = _unitOfWork.Users.Get(1, 1).First().Id;
            var secondUserId = _unitOfWork.Users.Get(2, 1).First().Id;

            var i = 0;

            foreach (var post in inputPosts.Posts)
            {
                var interest = _unitOfWork.Interests.Find(x => x.Name.Equals(post.Interests[0])).First();

                _unitOfWork.Posts.Add(new Post {
                    Description = post.Description,
                    SourceUrl = post.SourceUrl,
                    Title = post.Title,
                    UserId = (i % 2 == 0) ? firstUserId : secondUserId,
                    PostInterests = new List<PostInterest> { new PostInterest { InterestId = interest.Id } }

                });

                i++;
                _unitOfWork.Complete();
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
