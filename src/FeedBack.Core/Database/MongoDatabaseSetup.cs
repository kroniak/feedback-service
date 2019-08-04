using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FeedBack.Core.Models;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;

namespace FeedBack.Core.Database
{
    /// <summary>
    /// This class initialized MongoDatabase
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class MongoDatabaseSetup
    {
        public static IMongoDatabase Init(this IMongoDatabase database, string adminPass, string userPass)
        {
            var users = database.GetCollection<User>(DbConstants.UserCollectionName);
            var polls = database.GetCollection<Poll>(DbConstants.PollCollectionName);
            var directions = database.GetCollection<Direction>(DbConstants.DirectionCollectionName);
            var employees = database.GetCollection<Employer>(DbConstants.EmployersCollectionName);
            var candidates = database.GetCollection<Candidate>(DbConstants.CandidatesCollectionName);

            try
            {
                CreateIndexes(users, polls, directions, employees, candidates);

                if (users.Find(_ => true).FirstOrDefault() == null)
                {
                    InsertCredentials(users, adminPass, userPass);
                }
            }
            catch (Exception e)
            {
                throw new SystemException("Fail to init DB", e);
            }

            return database;
        }

        private static void InsertCredentials(
            IMongoCollection<User> users,
            string adminPass,
            string userPass)
        {
            var cs = new[]
            {
                new User
                {
                    UserName = "admin",
                    Roles = new[] {Role.Administrator, Role.User, Role.Editor, Role.Reviewer}
                },
                new User
                {
                    UserName = "user",
                    Roles = new[] {Role.User}
                }
            };

            foreach (var user in cs)
            {
                user.HashedPassword = GetHash(user, adminPass, userPass);
            }

            users.InsertMany(cs);
        }

        private static string GetHash(User user, string adminPass,
            string userPass) =>
            new PasswordHasher<User>().HashPassword(user,
                user.Roles.Any(r => r == Role.Administrator) ? adminPass : userPass);

        private static void CreateIndexes(
            IMongoCollection<User> users,
            IMongoCollection<Poll> polls,
            IMongoCollection<Direction> directions,
            IMongoCollection<Employer> employees,
            IMongoCollection<Candidate> candidates)
        {
            var optionsUnique = new CreateIndexOptions
            {
                Unique = true,
                Background = true
            };

            var optionsBackground = new CreateIndexOptions
            {
                Background = true
            };

            var userIndexModel1 = new CreateIndexModel<User>(
                Builders<User>.IndexKeys.Ascending(x => x.UserName),
                optionsUnique);

            var userIndexModel2 = new CreateIndexModel<User>(
                Builders<User>.IndexKeys.Ascending(x => x.Employer),
                optionsBackground);

            var employerIndexModel1 = new CreateIndexModel<Employer>(
                Builders<Employer>.IndexKeys.Ascending(x => x.User),
                optionsUnique);

            var employerIndexModel2 = new CreateIndexModel<Employer>(
                Builders<Employer>.IndexKeys.Ascending(x => x.TeamsPriorityFirst),
                optionsBackground);

            var employerIndexModel3 = new CreateIndexModel<Employer>(
                Builders<Employer>.IndexKeys.Ascending(x => x.TeamsPrioritySecond),
                optionsBackground);

            var employerIndexModel4 = new CreateIndexModel<Employer>(
                Builders<Employer>.IndexKeys.Ascending(x => x.Fio),
                optionsUnique);

            var candidateIndexModel = new CreateIndexModel<Candidate>(
                Builders<Candidate>.IndexKeys.Ascending(x => x.Fio),
                optionsUnique);

            var pollIndexModel = new CreateIndexModel<Poll>(
                Builders<Poll>.IndexKeys.Ascending(x => x.Owner),
                optionsBackground);

            var directionIndexModel1 = new CreateIndexModel<Direction>(
                Builders<Direction>.IndexKeys.Ascending(x => x.Poll),
                optionsBackground);

            var directionIndexModel2 = new CreateIndexModel<Direction>(
                Builders<Direction>.IndexKeys.Ascending(x => x.DirectedTo),
                optionsBackground);

            users.Indexes.CreateMany(new[]
            {
                userIndexModel1,
                userIndexModel2
            });
            employees.Indexes.CreateMany(new[]
            {
                employerIndexModel1,
                employerIndexModel2,
                employerIndexModel3,
                employerIndexModel4
            });
            directions.Indexes.CreateMany(new[]
            {
                directionIndexModel1,
                directionIndexModel2
            });
            candidates.Indexes.CreateOne(candidateIndexModel);
            polls.Indexes.CreateOne(pollIndexModel);
        }
    }
}