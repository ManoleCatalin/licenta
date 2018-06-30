using Core.Domain;
using Core.Interfaces;
using Core.Ordering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;


        public UnitOfWork(DbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Posts = new PostRepository(_context);
            Interests = new InterestRepository(_context);
            PostInterests = new PostInterestRepository(_context);
            Likes = new LikeRepository(_context);
            Favorites = new FavoriteRepository(_context);
            UserInterests = new Repository<UserInterest>(_context);
        }

        public IUserRepository Users { get; private set; }
        public IPostRepository Posts { get; private set; }
        public IInterestRepository Interests { get; private set; }
        public IPostInterestRepository PostInterests { get; private set; }
        public ILikeRepository Likes { get; private set; }
        public IFavoriteRepository Favorites { get; private set; }
        public IRepository<UserInterest> UserInterests { get; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IEnumerable<Post> GetPostsForInterest(Guid interestId, int pageIndex = 1, int pageSize = 1)
        {
            return Posts.GetPostsForInterest(interestId, pageIndex, pageSize);

        }

        public IEnumerable<Post> GetPostsForUser(Guid userId, bool selfPosts, int pageIndex = 1, int pageSize = 1, Ordering<Post> ordering = null, Guid? interestId = null)
        {
            IEnumerable<Post> posts = null;
            if (interestId != null)
            {
                posts = Posts.GetPostsForInterest((Guid)interestId, pageIndex, pageSize);
            } else
            {
                posts = Posts.Get(userId, selfPosts, pageIndex, pageSize, ordering);
            }

            return posts;
        }
    }
}
