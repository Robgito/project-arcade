using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Project_3___Arcade
{
    public static class Datamanager
    {
        //Database
        //public static Gebruiker GetGebruiker(int eenGebruikersID)
        //{
        //    using (var r = new ArcadeDBEntities())
        //    {
        //        var query = from gebruikers in r.Gebruikers
        //                    where gebruikers.UserID == eenGebruikersID
        //                    select gebruikers;

        //        return query.FirstOrDefault();
        //    }
        //}

        //public static List<Gebruiker> GetGebruikers()
        //{
        //    using (var x = new ArcadeDBEntities())
        //    {
        //        return x.Gebruikers.ToList();
        //    }
        //}

        //public static bool UpdateGebruiker(Gebruiker eenGebruiker)
        //{
        //    bool updateSucceeded = false;
        //    using (var x = new ArcadeDBEntities())
        //    {
        //        x.Entry(eenGebruiker).State = System.Data.Entity.EntityState.Modified;
        //        if (0 < x.SaveChanges())
        //        {
        //            updateSucceeded = true;
        //        }
        //    }
        //    return updateSucceeded;
        //}

        //public static bool DeleteGebruiker(Gebruiker eenGebruiker)
        //{
        //    bool deleteSucceeded = false;
        //    using (var x = new ArcadeDBEntities())
        //    {
        //        x.Entry(eenGebruiker).State = System.Data.Entity.EntityState.Deleted;
        //        if (0 < x.SaveChanges())
        //        {
        //            deleteSucceeded = true;
        //        }
        //    }
        //    return deleteSucceeded;
        //}

        //public static bool InsertGebruiker(Gebruiker eenGebruiker)
        //{
        //    bool insertSucceeded = false;
        //    using (var x = new ArcadeDBEntities())
        //    {
        //        x.Gebruikers.Add(eenGebruiker);
        //        if (0 < x.SaveChanges())
        //        {
        //            insertSucceeded = true;
        //        }
        //    }
        //    return insertSucceeded;
        //}

        ////Highscores
        ////Tetris
        //public static ScoreTetris GetScoreTetrisGebruiker(int eenGebruikersID)
        //{
        //    using (var r = new ArcadeDBEntities())
        //    {
        //        var query = from scoresTetris in r.ScoresTetris
        //                    where scoresTetris.FKGebruiker == eenGebruikersID
        //                    select scoresTetris;

        //        return query.FirstOrDefault();
        //    }
        //}

        //public static List<ScoreTetris> GetScoresTetris()
        //{
        //    using (var x = new ArcadeDBEntities())
        //    {
        //        return x.ScoresTetris.ToList();
        //    }
        //}

        //public static bool InsertScoreTetris(ScoreTetris eenScore)
        //{
        //    bool insertSucceeded = false;
        //    using (var x = new ArcadeDBEntities())
        //    {
        //        x.ScoresTetris.Add(eenScore);
        //        if (0 < x.SaveChanges())
        //        {
        //            insertSucceeded = true;
        //        }
        //    }
        //    return insertSucceeded;
        //}

        //public static bool DeleteScoreTetris(ScoreTetris eenScore)
        //{
        //    bool deleteSucceeded = false;
        //    using (var x = new ArcadeDBEntities())
        //    {
        //        x.Entry(eenScore).State = System.Data.Entity.EntityState.Deleted;
        //        if (0 < x.SaveChanges())
        //        {
        //            deleteSucceeded = true;
        //        }
        //    }
        //    return deleteSucceeded;
        //}

        //public static bool UpdateScoreTetris(ScoreTetris eenScore)
        //{
        //    bool updateSucceeded = false;
        //    using (var x = new ArcadeDBEntities())
        //    {
        //        x.Entry(eenScore).State = System.Data.Entity.EntityState.Modified;
        //        if (0 < x.SaveChanges())
        //        {
        //            updateSucceeded = true;
        //        }
        //    }
        //    return updateSucceeded;
        //}

        ////Flappy Bird
        //public static ScoreFlappyBird GetScoreFlappyBirdGebruiker(int eenGebruikersID)
        //{
        //    using (var r = new ArcadeDBEntities())
        //    {
        //        var query = from scoresFlappyBird in r.ScoresFlappyBird
        //                    where scoresFlappyBird.FKGebruiker == eenGebruikersID
        //                    select scoresFlappyBird;

        //        return query.FirstOrDefault();
        //    }
        //}

        //public static List<ScoreFlappyBird> GetScoresFlappyBird()
        //{
        //    using (var x = new ArcadeDBEntities())
        //    {
        //        return x.ScoresFlappyBird.ToList();
        //    }
        //}

        //public static bool InsertScoreFlappyBird(ScoreFlappyBird eenScore)
        //{
        //    bool insertSucceeded = false;
        //    using (var x = new ArcadeDBEntities())
        //    {
        //        x.ScoresFlappyBird.Add(eenScore);
        //        if (0 < x.SaveChanges())
        //        {
        //            insertSucceeded = true;
        //        }
        //    }
        //    return insertSucceeded;
        //}

        //public static bool DeleteScoreFlappyBird(ScoreFlappyBird eenScore)
        //{
        //    bool deleteSucceeded = false;
        //    using (var x = new ArcadeDBEntities())
        //    {
        //        x.Entry(eenScore).State = System.Data.Entity.EntityState.Deleted;
        //        if (0 < x.SaveChanges())
        //        {
        //            deleteSucceeded = true;
        //        }
        //    }
        //    return deleteSucceeded;
        //}

        //public static bool UpdateScoreFlappyBird(ScoreFlappyBird eenScore)
        //{
        //    bool updateSucceeded = false;
        //    using (var x = new ArcadeDBEntities())
        //    {
        //        x.Entry(eenScore).State = System.Data.Entity.EntityState.Modified;
        //        if (0 < x.SaveChanges())
        //        {
        //            updateSucceeded = true;
        //        }
        //    }
        //    return updateSucceeded;
        //}

        ////Endless Runner
        //public static ScoreEndlessRunner GetScoreEndlessRunnerGebruiker(int eenGebruikersID)
        //{
        //    using (var r = new ArcadeDBEntities())
        //    {
        //        var query = from scoresEndlessRunner in r.ScoresEndlessRunner
        //                    where scoresEndlessRunner.FKGebruiker == eenGebruikersID
        //                    select scoresEndlessRunner;

        //        return query.FirstOrDefault();
        //    }
        //}

        //public static List<ScoreEndlessRunner> GetScoresEndlessRunner()
        //{
        //    using (var x = new ArcadeDBEntities())
        //    {
        //        return x.ScoresEndlessRunner.ToList();
        //    }
        //}

        //public static bool InsertScoreEndlessRunner(ScoreEndlessRunner eenScore)
        //{
        //    bool insertSucceeded = false;
        //    using (var x = new ArcadeDBEntities())
        //    {
        //        x.ScoresEndlessRunner.Add(eenScore);
        //        if (0 < x.SaveChanges())
        //        {
        //            insertSucceeded = true;
        //        }
        //    }
        //    return insertSucceeded;
        //}

        //public static bool DeleteScoreEndlessRunner(ScoreEndlessRunner eenScore)
        //{
        //    bool deleteSucceeded = false;
        //    using (var x = new ArcadeDBEntities())
        //    {
        //        x.Entry(eenScore).State = System.Data.Entity.EntityState.Deleted;
        //        if (0 < x.SaveChanges())
        //        {
        //            deleteSucceeded = true;
        //        }
        //    }
        //    return deleteSucceeded;
        //}

        //public static bool UpdateScoreEndlessRunner(ScoreEndlessRunner eenScore)
        //{
        //    bool updateSucceeded = false;
        //    using (var x = new ArcadeDBEntities())
        //    {
        //        x.Entry(eenScore).State = System.Data.Entity.EntityState.Modified;
        //        if (0 < x.SaveChanges())
        //        {
        //            updateSucceeded = true;
        //        }
        //    }
        //    return updateSucceeded;
        //}

        ////Zombie Shooter
        //public static ScoreZombieShooter GetScoreZombieShooterGebruiker(int eenGebruikersID)
        //{
        //    using (var r = new ArcadeDBEntities())
        //    {
        //        var query = from scoresZombieShooter in r.ScoresZombieShooter
        //                    where scoresZombieShooter.FKGebruiker == eenGebruikersID
        //                    select scoresZombieShooter;

        //        return query.FirstOrDefault();
        //    }
        //}

        //public static List<ScoreZombieShooter> GetScoresZombieShooter()
        //{
        //    using (var x = new ArcadeDBEntities())
        //    {
        //        return x.ScoresZombieShooter.ToList();
        //    }
        //}

        //public static bool InsertScoreZombieShooter(ScoreZombieShooter eenScore)
        //{
        //    bool insertSucceeded = false;
        //    using (var x = new ArcadeDBEntities())
        //    {
        //        x.ScoresZombieShooter.Add(eenScore);
        //        if (0 < x.SaveChanges())
        //        {
        //            insertSucceeded = true;
        //        }
        //    }
        //    return insertSucceeded;
        //}

        //public static bool DeleteScoreZombieShooter(ScoreZombieShooter eenScore)
        //{
        //    bool deleteSucceeded = false;
        //    using (var x = new ArcadeDBEntities())
        //    {
        //        x.Entry(eenScore).State = System.Data.Entity.EntityState.Deleted;
        //        if (0 < x.SaveChanges())
        //        {
        //            deleteSucceeded = true;
        //        }
        //    }
        //    return deleteSucceeded;
        //}

        //public static bool UpdateScoreZombieShooter(ScoreZombieShooter eenScore)
        //{
        //    bool updateSucceeded = false;
        //    using (var x = new ArcadeDBEntities())
        //    {
        //        x.Entry(eenScore).State = System.Data.Entity.EntityState.Modified;
        //        if (0 < x.SaveChanges())
        //        {
        //            updateSucceeded = true;
        //        }
        //    }
        //    return updateSucceeded;
        //}

        ////Snake
        //public static ScoreSnake GetScoreSnakeGebruiker(int eenGebruikersID)
        //{
        //    using (var r = new ArcadeDBEntities())
        //    {
        //        var query = from scoresSnake in r.ScoresSnake
        //                    where scoresSnake.FKGebruiker == eenGebruikersID
        //                    select scoresSnake;

        //        return query.FirstOrDefault();
        //    }
        //}

        //public static List<ScoreSnake> GetScoresSnake()
        //{
        //    using (var x = new ArcadeDBEntities())
        //    {
        //        return x.ScoresSnake.ToList();
        //    }
        //}

        //public static bool InsertScoreSnake(ScoreSnake eenScore)
        //{
        //    bool insertSucceeded = false;
        //    using (var x = new ArcadeDBEntities())
        //    {
        //        x.ScoresSnake.Add(eenScore);
        //        if (0 < x.SaveChanges())
        //        {
        //            insertSucceeded = true;
        //        }
        //    }
        //    return insertSucceeded;
        //}

        //public static bool DeleteScoreSnake(ScoreSnake eenScore)
        //{
        //    bool deleteSucceeded = false;
        //    using (var x = new ArcadeDBEntities())
        //    {
        //        x.Entry(eenScore).State = System.Data.Entity.EntityState.Deleted;
        //        if (0 < x.SaveChanges())
        //        {
        //            deleteSucceeded = true;
        //        }
        //    }
        //    return deleteSucceeded;
        //}

        //public static bool UpdateScoreSnake(ScoreSnake eenScore)
        //{
        //    bool updateSucceeded = false;
        //    using (var x = new ArcadeDBEntities())
        //    {
        //        x.Entry(eenScore).State = System.Data.Entity.EntityState.Modified;
        //        if (0 < x.SaveChanges())
        //        {
        //            updateSucceeded = true;
        //        }
        //    }
        //    return updateSucceeded;
        //}

        //JSON
        public static Gebruiker GetGebruiker(int eenGebruikersID)
        {
            List<Gebruiker> lijstGebruikers = GetGebruikers();
            Gebruiker gebruiker1 = null;

            if (lijstGebruikers != null)
            {
                foreach (Gebruiker gebruiker in lijstGebruikers)
                {
                    if (gebruiker.UserID == eenGebruikersID)
                    {
                        gebruiker1 = gebruiker;
                        break;
                    }
                }
            }
            return gebruiker1;
        }

        public static List<Gebruiker> GetGebruikers()
        {
            List<Gebruiker> lijstGebruikers = new List<Gebruiker>();

            if (System.IO.File.Exists("Gebruikers.json"))
            {
                using (StreamReader r = new StreamReader("Gebruikers.json"))
                {
                    string json = r.ReadToEnd();
                    lijstGebruikers = JsonSerializer.Deserialize<List<Gebruiker>>(json);
                }
            }
            else
            {
                /*throw new Exception("Er zijn geen gebruikers gevonden.");*/
                lijstGebruikers = null;
            }
            return lijstGebruikers;
        }

        public static bool UpdateGebruiker(Gebruiker eenGebruiker)
        {
            bool updateSucceeded = false;

            if (System.IO.File.Exists("Gebruikers.json"))
            {
                List<Gebruiker> lijstGebruikers = GetGebruikers();

                foreach (Gebruiker gebruiker in lijstGebruikers)
                {
                    if (gebruiker.UserID == eenGebruiker.UserID)
                    {
                        gebruiker.Gebruikersnaam = eenGebruiker.Gebruikersnaam;
                        gebruiker.Admin = eenGebruiker.Admin;
                        gebruiker.Avatar = eenGebruiker.Avatar;
                        gebruiker.Credits = eenGebruiker.Credits;
                        gebruiker.Email = eenGebruiker.Email;
                        gebruiker.Paswoord = eenGebruiker.Paswoord;


                        JsonSerializerOptions options = new JsonSerializerOptions();
                        options.WriteIndented = true;
                        string json = JsonSerializer.Serialize(lijstGebruikers, options);
                        File.WriteAllText("Gebruikers.json", json);

                        updateSucceeded = true;
                    }
                }
            }
            return updateSucceeded;
        }

        public static bool DeleteGebruiker(Gebruiker eenGebruiker)
        {
            bool deleteSucceeded = false;

            if (System.IO.File.Exists("Gebruikers.json"))
            {
                List<Gebruiker> lijstGebruikers = GetGebruikers();

                foreach (Gebruiker gebruiker in lijstGebruikers)
                {
                    if (gebruiker.UserID == eenGebruiker.UserID)
                    {
                        lijstGebruikers.Remove(gebruiker);

                        JsonSerializerOptions options = new JsonSerializerOptions();
                        options.WriteIndented = true;
                        string json = JsonSerializer.Serialize(lijstGebruikers, options);
                        File.WriteAllText("Gebruikers.json", json);

                        deleteSucceeded = true;
                        break;
                    }
                }
            }
            return deleteSucceeded;
        }

        public static bool InsertGebruiker(Gebruiker eenGebruiker)
        {
            bool insertSucceeded = false;

            if (System.IO.File.Exists("Gebruikers.json"))
            {
                List<Gebruiker> lijstGebruikers = GetGebruikers();
                eenGebruiker.UserID = lijstGebruikers[lijstGebruikers.Count - 1].UserID + 1;
                lijstGebruikers.Add(eenGebruiker);

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                string json = JsonSerializer.Serialize(lijstGebruikers, options);
                File.WriteAllText("Gebruikers.json", json);

                insertSucceeded = true;
            }
            else
            {
                List<Gebruiker> lijstGebruikers = new List<Gebruiker>();
                lijstGebruikers.Add(eenGebruiker);

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                string json = JsonSerializer.Serialize(lijstGebruikers, options);
                File.WriteAllText("Gebruikers.json", json);

                insertSucceeded = true;
            }
            return insertSucceeded;
        }

        //Highscores
        //Tetris
        public static ScoreTetris GetScoreTetrisGebruiker(int eenGebruikersID)
        {
            List<ScoreTetris> lijstScores = GetScoresTetris();
            ScoreTetris score1 = null;

            if (lijstScores != null)
            {
                foreach (ScoreTetris score in lijstScores)
                {
                    if (score.FKGebruiker == eenGebruikersID)
                    {
                        score1 = score;
                        break;
                    }
                }
            }
            return score1;
        }

        public static List<ScoreTetris> GetScoresTetris()
        {
            List<ScoreTetris> lijstScores = new List<ScoreTetris>();

            if (System.IO.File.Exists("ScoresTetris.json"))
            {
                using (StreamReader r = new StreamReader("ScoresTetris.json"))
                {
                    string json = r.ReadToEnd();
                    lijstScores = JsonSerializer.Deserialize<List<ScoreTetris>>(json);
                }
            }
            else
            {
                lijstScores = null;
            }
            return lijstScores;
        }

        public static bool InsertScoreTetris(ScoreTetris eenScore)
        {
            bool insertSucceeded = false;

            if (System.IO.File.Exists("ScoresTetris.json"))
            {
                List<ScoreTetris> lijstScores = GetScoresTetris();
                eenScore.ScoreID = lijstScores[lijstScores.Count - 1].ScoreID + 1;

                lijstScores.Add(eenScore);

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                string json = JsonSerializer.Serialize(lijstScores, options);
                File.WriteAllText("ScoresTetris.json", json);

                insertSucceeded = true;
            }
            else
            {
                List<ScoreTetris> lijstScores = new List<ScoreTetris>();
                lijstScores.Add(eenScore);

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                string json = JsonSerializer.Serialize(lijstScores, options);
                File.WriteAllText("ScoresTetris.json", json);

                insertSucceeded = true;
            }
            return insertSucceeded;
        }

        public static bool DeleteScoreTetris(ScoreTetris eenScore)
        {
            bool deleteSucceeded = false;

            if (System.IO.File.Exists("ScoresTetris.json"))
            {
                List<ScoreTetris> lijstScores = new List<ScoreTetris>();

                foreach (ScoreTetris score in lijstScores)
                {
                    if (score.ScoreID == eenScore.ScoreID)
                    {
                        lijstScores.Remove(score);

                        JsonSerializerOptions options = new JsonSerializerOptions();
                        options.WriteIndented = true;
                        string json = JsonSerializer.Serialize(lijstScores, options);
                        File.WriteAllText("ScoresTetris.json", json);

                        deleteSucceeded = true;
                        break;
                    }
                }
            }
            return deleteSucceeded;
        }

        public static bool UpdateScoreTetris(ScoreTetris eenScore)
        {
            bool updateSucceeded = false;

            if (System.IO.File.Exists("ScoresTetris.json"))
            {
                List<ScoreTetris> lijstScores = GetScoresTetris();

                foreach (ScoreTetris score in lijstScores)
                {
                    if (score.ScoreID == eenScore.ScoreID)
                    {
                        score.Score = eenScore.Score;

                        JsonSerializerOptions options = new JsonSerializerOptions();
                        options.WriteIndented = true;
                        string json = JsonSerializer.Serialize(lijstScores, options);
                        File.WriteAllText("ScoresTetris.json", json);

                        updateSucceeded = true;
                    }
                }
            }
            return updateSucceeded;
        }

        //Flappy Bird
        public static ScoreFlappyBird GetScoreFlappyBirdGebruiker(int eenGebruikersID)
        {
            List<ScoreFlappyBird> lijstScores = GetScoresFlappyBird();
            ScoreFlappyBird score1 = null;

            if (lijstScores != null)
            {
                foreach (ScoreFlappyBird score in lijstScores)
                {
                    if (score.FKGebruiker == eenGebruikersID)
                    {
                        score1 = score;
                        break;
                    }
                }
            }
            return score1;
        }

        public static List<ScoreFlappyBird> GetScoresFlappyBird()
        {
            List<ScoreFlappyBird> lijstScores = new List<ScoreFlappyBird>();

            if (System.IO.File.Exists("ScoresFlappyBird.json"))
            {
                using (StreamReader r = new StreamReader("ScoresFlappyBird.json"))
                {
                    string json = r.ReadToEnd();
                    lijstScores = JsonSerializer.Deserialize<List<ScoreFlappyBird>>(json);
                }
            }
            else
            {
                lijstScores = null;
            }
            return lijstScores;
        }

        public static bool InsertScoreFlappyBird(ScoreFlappyBird eenScore)
        {
            bool insertSucceeded = false;

            if (System.IO.File.Exists("ScoresFlappyBird.json"))
            {
                List<ScoreFlappyBird> lijstScores = GetScoresFlappyBird();
                eenScore.ScoreID = lijstScores[lijstScores.Count - 1].ScoreID + 1;

                lijstScores.Add(eenScore);

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                string json = JsonSerializer.Serialize(lijstScores, options);
                File.WriteAllText("ScoresFlappyBird.json", json);

                insertSucceeded = true;
            }
            else
            {
                List<ScoreFlappyBird> lijstScores = new List<ScoreFlappyBird>();
                lijstScores.Add(eenScore);

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                string json = JsonSerializer.Serialize(lijstScores, options);
                File.WriteAllText("ScoresFlappyBird.json", json);

                insertSucceeded = true;
            }
            return insertSucceeded;
        }

        public static bool DeleteScoreFlappyBird(ScoreFlappyBird eenScore)
        {
            bool deleteSucceeded = false;

            if (System.IO.File.Exists("ScoresFlappyBird.json"))
            {
                List<ScoreFlappyBird> lijstScores = new List<ScoreFlappyBird>();

                foreach (ScoreFlappyBird score in lijstScores)
                {
                    if (score.ScoreID == eenScore.ScoreID)
                    {
                        lijstScores.Remove(score);

                        JsonSerializerOptions options = new JsonSerializerOptions();
                        options.WriteIndented = true;
                        string json = JsonSerializer.Serialize(lijstScores, options);
                        File.WriteAllText("ScoresFlappyBird.json", json);

                        deleteSucceeded = true;
                        break;
                    }
                }
            }
            return deleteSucceeded;
        }

        public static bool UpdateScoreFlappyBird(ScoreFlappyBird eenScore)
        {
            bool updateSucceeded = false;

            if (System.IO.File.Exists("ScoresFlappyBird.json"))
            {
                List<ScoreFlappyBird> lijstScores = GetScoresFlappyBird();

                foreach (ScoreFlappyBird score in lijstScores)
                {
                    if (score.ScoreID == eenScore.ScoreID)
                    {
                        score.Score = eenScore.Score;

                        JsonSerializerOptions options = new JsonSerializerOptions();
                        options.WriteIndented = true;
                        string json = JsonSerializer.Serialize(lijstScores, options);
                        File.WriteAllText("ScoresFlappyBird.json", json);

                        updateSucceeded = true;
                    }
                }
            }
            return updateSucceeded;
        }

        //Endless Runner
        public static ScoreEndlessRunner GetScoreEndlessRunnerGebruiker(int eenGebruikersID)
        {
            List<ScoreEndlessRunner> lijstScores = GetScoresEndlessRunner();
            ScoreEndlessRunner score1 = null;

            if (lijstScores != null)
            {
                foreach (ScoreEndlessRunner score in lijstScores)
                {
                    if (score.FKGebruiker == eenGebruikersID)
                    {
                        score1 = score;
                        break;
                    }
                }
            }
            return score1;
        }

        public static List<ScoreEndlessRunner> GetScoresEndlessRunner()
        {
            List<ScoreEndlessRunner> lijstScores = new List<ScoreEndlessRunner>();

            if (System.IO.File.Exists("ScoresEndlessRunner.json"))
            {
                using (StreamReader r = new StreamReader("ScoresEndlessRunner.json"))
                {
                    string json = r.ReadToEnd();
                    lijstScores = JsonSerializer.Deserialize<List<ScoreEndlessRunner>>(json);
                }
            }
            else
            {
                lijstScores = null;
            }
            return lijstScores;
        }

        public static bool InsertScoreEndlessRunner(ScoreEndlessRunner eenScore)
        {
            bool insertSucceeded = false;

            if (System.IO.File.Exists("ScoresEndlessRunner.json"))
            {
                List<ScoreEndlessRunner> lijstScores = GetScoresEndlessRunner();
                eenScore.ScoreID = lijstScores[lijstScores.Count - 1].ScoreID + 1;

                lijstScores.Add(eenScore);

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                string json = JsonSerializer.Serialize(lijstScores, options);
                File.WriteAllText("ScoresEndlessRunner.json", json);

                insertSucceeded = true;
            }
            else
            {
                List<ScoreEndlessRunner> lijstScores = new List<ScoreEndlessRunner>();
                lijstScores.Add(eenScore);

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                string json = JsonSerializer.Serialize(lijstScores, options);
                File.WriteAllText("ScoresEndlessRunner.json", json);

                insertSucceeded = true;
            }
            return insertSucceeded;
        }

        public static bool DeleteScoreEndlessRunner(ScoreEndlessRunner eenScore)
        {
            bool deleteSucceeded = false;

            if (System.IO.File.Exists("ScoresEndlessRunner.json"))
            {
                List<ScoreEndlessRunner> lijstScores = new List<ScoreEndlessRunner>();

                foreach (ScoreEndlessRunner score in lijstScores)
                {
                    if (score.ScoreID == eenScore.ScoreID)
                    {
                        lijstScores.Remove(score);

                        JsonSerializerOptions options = new JsonSerializerOptions();
                        options.WriteIndented = true;
                        string json = JsonSerializer.Serialize(lijstScores, options);
                        File.WriteAllText("ScoresEndlessRunner.json", json);

                        deleteSucceeded = true;
                        break;
                    }
                }
            }
            return deleteSucceeded;
        }

        public static bool UpdateScoreEndlessRunner(ScoreEndlessRunner eenScore)
        {
            bool updateSucceeded = false;

            if (System.IO.File.Exists("ScoresEndlessRunner.json"))
            {
                List<ScoreEndlessRunner> lijstScores = GetScoresEndlessRunner();

                foreach (ScoreEndlessRunner score in lijstScores)
                {
                    if (score.ScoreID == eenScore.ScoreID)
                    {
                        score.Score = eenScore.Score;

                        JsonSerializerOptions options = new JsonSerializerOptions();
                        options.WriteIndented = true;
                        string json = JsonSerializer.Serialize(lijstScores, options);
                        File.WriteAllText("ScoresEndlessRunner.json", json);

                        updateSucceeded = true;
                    }
                }
            }
            return updateSucceeded;
        }

        //Zombie Shooter
        public static ScoreZombieShooter GetScoreZombieShooterGebruiker(int eenGebruikersID)
        {
            List<ScoreZombieShooter> lijstScores = GetScoresZombieShooter();
            ScoreZombieShooter score1 = null;

            if (lijstScores != null)
            {
                foreach (ScoreZombieShooter score in lijstScores)
                {
                    if (score.FKGebruiker == eenGebruikersID)
                    {
                        score1 = score;
                        break;
                    }
                }
            }
            return score1;
        }

        public static List<ScoreZombieShooter> GetScoresZombieShooter()
        {
            List<ScoreZombieShooter> lijstScores = new List<ScoreZombieShooter>();

            if (System.IO.File.Exists("ScoresZombieShooter.json"))
            {
                using (StreamReader r = new StreamReader("ScoresZombieShooter.json"))
                {
                    string json = r.ReadToEnd();
                    lijstScores = JsonSerializer.Deserialize<List<ScoreZombieShooter>>(json);
                }
            }
            else
            {
                lijstScores = null;
            }
            return lijstScores;
        }

        public static bool InsertScoreZombieShooter(ScoreZombieShooter eenScore)
        {
            bool insertSucceeded = false;

            if (System.IO.File.Exists("ScoresZombieShooter.json"))
            {
                List<ScoreZombieShooter> lijstScores = GetScoresZombieShooter();
                eenScore.ScoreID = lijstScores[lijstScores.Count - 1].ScoreID + 1;

                lijstScores.Add(eenScore);

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                string json = JsonSerializer.Serialize(lijstScores, options);
                File.WriteAllText("ScoresZombieShooter.json", json);

                insertSucceeded = true;
            }
            else
            {
                List<ScoreZombieShooter> lijstScores = new List<ScoreZombieShooter>();
                lijstScores.Add(eenScore);

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                string json = JsonSerializer.Serialize(lijstScores, options);
                File.WriteAllText("ScoresZombieShooter.json", json);

                insertSucceeded = true;
            }
            return insertSucceeded;
        }

        public static bool DeleteScoreZombieShooter(ScoreZombieShooter eenScore)
        {
            bool deleteSucceeded = false;

            if (System.IO.File.Exists("ScoresZombieShooter.json"))
            {
                List<ScoreZombieShooter> lijstScores = new List<ScoreZombieShooter>();

                foreach (ScoreZombieShooter score in lijstScores)
                {
                    if (score.ScoreID == eenScore.ScoreID)
                    {
                        lijstScores.Remove(score);

                        JsonSerializerOptions options = new JsonSerializerOptions();
                        options.WriteIndented = true;
                        string json = JsonSerializer.Serialize(lijstScores, options);
                        File.WriteAllText("ScoresZombieShooter.json", json);

                        deleteSucceeded = true;
                        break;
                    }
                }
            }
            return deleteSucceeded;
        }

        public static bool UpdateScoreZombieShooter(ScoreZombieShooter eenScore)
        {
            bool updateSucceeded = false;

            if (System.IO.File.Exists("ScoresZombieShooter.json"))
            {
                List<ScoreZombieShooter> lijstScores = GetScoresZombieShooter();

                foreach (ScoreZombieShooter score in lijstScores)
                {
                    if (score.ScoreID == eenScore.ScoreID)
                    {
                        score.Score = eenScore.Score;

                        JsonSerializerOptions options = new JsonSerializerOptions();
                        options.WriteIndented = true;
                        string json = JsonSerializer.Serialize(lijstScores, options);
                        File.WriteAllText("ScoresZombieShooter.json", json);

                        updateSucceeded = true;
                    }
                }
            }
            return updateSucceeded;
        }

        //Snake
        public static ScoreSnake GetScoreSnakeGebruiker(int eenGebruikersID)
        {
            List<ScoreSnake> lijstScores = GetScoresSnake();
            ScoreSnake score1 = null;

            if (lijstScores != null)
            {
                foreach (ScoreSnake score in lijstScores)
                {
                    if (score.FKGebruiker == eenGebruikersID)
                    {
                        score1 = score;
                        break;
                    }
                }
            }
            return score1;
        }

        public static List<ScoreSnake> GetScoresSnake()
        {
            List<ScoreSnake> lijstScores = new List<ScoreSnake>();

            if (System.IO.File.Exists("ScoresSnake.json"))
            {
                using (StreamReader r = new StreamReader("ScoresSnake.json"))
                {
                    string json = r.ReadToEnd();
                    lijstScores = JsonSerializer.Deserialize<List<ScoreSnake>>(json);
                }
            }
            else
            {
                lijstScores = null;
            }
            return lijstScores;
        }

        public static bool InsertScoreSnake(ScoreSnake eenScore)
        {
            bool insertSucceeded = false;

            if (System.IO.File.Exists("ScoresSnake.json"))
            {
                List<ScoreSnake> lijstScores = GetScoresSnake();
                eenScore.ScoreID = lijstScores[lijstScores.Count - 1].ScoreID + 1;

                lijstScores.Add(eenScore);

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                string json = JsonSerializer.Serialize(lijstScores, options);
                File.WriteAllText("ScoresSnake.json", json);

                insertSucceeded = true;
            }
            else
            {
                List<ScoreSnake> lijstScores = new List<ScoreSnake>();
                lijstScores.Add(eenScore);

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                string json = JsonSerializer.Serialize(lijstScores, options);
                File.WriteAllText("ScoresSnake.json", json);

                insertSucceeded = true;
            }
            return insertSucceeded;
        }

        public static bool DeleteScoreSnake(ScoreSnake eenScore)
        {
            bool deleteSucceeded = false;

            if (System.IO.File.Exists("ScoresSnake.json"))
            {
                List<ScoreSnake> lijstScores = new List<ScoreSnake>();

                foreach (ScoreSnake score in lijstScores)
                {
                    if (score.ScoreID == eenScore.ScoreID)
                    {
                        lijstScores.Remove(score);

                        JsonSerializerOptions options = new JsonSerializerOptions();
                        options.WriteIndented = true;
                        string json = JsonSerializer.Serialize(lijstScores, options);
                        File.WriteAllText("ScoresSnake.json", json);

                        deleteSucceeded = true;
                        break;
                    }
                }
            }
            return deleteSucceeded;
        }

        public static bool UpdateScoreSnake(ScoreSnake eenScore)
        {
            bool updateSucceeded = false;

            if (System.IO.File.Exists("ScoresSnake.json"))
            {
                List<ScoreSnake> lijstScores = GetScoresSnake();

                foreach (ScoreSnake score in lijstScores)
                {
                    if (score.ScoreID == eenScore.ScoreID)
                    {
                        score.Score = eenScore.Score;

                        JsonSerializerOptions options = new JsonSerializerOptions();
                        options.WriteIndented = true;
                        string json = JsonSerializer.Serialize(lijstScores, options);
                        File.WriteAllText("ScoresSnake.json", json);

                        updateSucceeded = true;
                    }
                }
            }
            return updateSucceeded;
        }
    }
}
