﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithDB.Abstract;
using WorkWithDB.Entity;
using WorkWithDB.Repository.Infrastructure;

namespace WorkWithDB.Repository
{
    internal class BlogUserRepository : BaseRepository<int, BlogUser>, IBlogUserRepository
    {
        public BlogUserRepository(SqlConnection connection):base(connection){}

        public override int Insert(Entity.BlogUser entity)
        {
            return
                base.ExecuteScalar<int>(
                        "insert into BlogUser (UserPassword,Name,Nick) values (@UserPassword,@Name,@Nick)",
                        new SqlParameters
                        {
                            {"UserPassword", entity.UserPassword},
                            {"Name", entity.Name                },
                            {"Nick", entity.Nick                },
                        }
                    );
        }

        public override bool Update(Entity.BlogUser entity)
        {
            var res = base.ExecuteNonQuery(
                    "UPDATE BlogUser set UserPassword = @UserPassword,   Name = @Name,  Nick = @Nick where Id = @Id ",
                    new SqlParameters
                    {
                        {"UserPassword", entity.UserPassword},
                        {"Name", entity.Name                },
                        {"Nick", entity.Nick                },
                        {"Id", entity.Id                    },
                    }
                );

            return res > 0;
        }



        public int GetCount()
        {
            return base.ExecuteScalar<int>("select count(*) from BlogUser");
        }

        public Entity.BlogUser GetById(int id)
        {
            return base.ExecuteSingleRowSelect(
                    "select bu.Id,bu.Name,bu.Nick,bu.UserPassword from  BlogUser bu where bu.Id = @userId",
                    new SqlParameters()
                    {
                        {"userId",id}
                    }
                );
        }

        public bool Delete(int id)
        {
            var res = base.ExecuteNonQuery(
                "delete from BlogUser where Id = @id",
                new SqlParameters() { { "id", id } });

            if (res > 1)
                throw new InvalidOperationException("Multiple rows deleted by single delete query");

            return res == 1;
        }


        protected override BlogUser DefaultRowMapping(SqlDataReader reader)
        {
            var user = new BlogUser
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Name"],
                Nick = (string)reader["Nick"]
            };

            return user;
        }
    }
}