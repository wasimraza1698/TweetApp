namespace TweetApp.Common
{
    internal class Constants
    {
        // Endpoint Urls
        internal const string BaseURL = "api/v1.0/tweets";
        internal const string RegisterUser = "register";
        internal const string Login = "login";
        internal const string SearchUser = "search/{username}";
        internal const string AllUsers = "users/all";
        internal const string AddTweet = "{username}/add";
        internal const string AllTweets = "all";
        internal const string AllTweetsOfUser = "{username}";
        internal const string UpdateTweet = "{username}/update/{tweetid}";
        internal const string AddReply = "{username}/reply/{tweetid}";
        internal const string LikeTweet = "{username}/like/{tweetid}";
        internal const string DeleteTweet = "{username}/delete/{tweetid}";
        internal const string GetTweet = "tweets/{tweetid}";

        // Response Messages
        internal const string UserCreated = "User created with username {0}.";
        internal const string ExistingUser = "User existing with given emailid or username. Please login or give different username and emailid to register.";
        internal const string InvalidCredentials = "Invalid username or password.";
        internal const string LoginSuccess = "Logged in successfully.";
        internal const string TweetAdded = "Tweet posted.";
        internal const string FailedPostingTweet = "Could not post tweet.";
        internal const string ReplyAdded = "Reply added to tweet.";
        internal const string FailedAddingReply = "Failed adding reply to tweet.";
        internal const string FailedUpdatingTweet = "Failed updating tweet.";
        internal const string FailedGettingTweets = "Failed getting tweets.";
        internal const string FailedGettingTweet = "Failed getting tweet.";
        internal const string TweetDeleted = "Tweet is deleted.";
        internal const string FailedDeletingTweet = "Failed deleting tweet";
        internal const string FailedLikingTweet = "Failed liking tweet";

        // Error Logss
        internal const string RegistrationError = "Error occurred while registering user: {0}. Message: {1}, StackTrace: {2}";
        internal const string SearchUserError = "Error occurred while searching for user: {0}. Message: {1}, StackTrace: {2}";
        internal const string LoginError = "Error occurred while logging in user: {0}. Message: {1}, StackTrace: {2}";
        internal const string GetAllUsersError = "Error occurred while retrieving all the users. Message: {0}, StackTrace: {1}";
        internal const string AddingTweetError = "Error occurred while adding a tweet for user: {0}. Message: {1}, StackTrace: {2}";
        internal const string GetAllTweetsError = "Error occurred while getting all the tweets. Message: {0}, StackTrace: {1}";
        internal const string GetAllTweetsOfUserError = "Error occurred while getting all the tweets of user: {0}. Message: {1}, StackTrace: {2}";
        internal const string AddReplyError = "Error occurred while adding reply to tweet with id: {0} by user: {1}. Message: {2}, StackTrace: {3}";
        internal const string UpdateTweetError = "Error occurred while updating tweet with id: {0} by user: {1}. Message: {2}, StackTrace: {3}";
        internal const string DeleteTweetError = "Error occurred while deleting tweet with id: {0} by user: {1}. Message: {2}, StackTrace: {3}";
        internal const string LikingTweetError = "Error occurred while liking tweet with id: {0} by user: {1}. Message: {2}, StackTrace: {3}";
        internal const string GetTweetError = "Error occurred while fetching tweet with id: {0}. Message: {1}, StackTrace: {2}";

        // User Service Logs
        internal const string SearchingUserByUserName = "Searching user by username: {0}";
        internal const string SearchingUserByEmailId = "Searching user by emailid: {0}";
        internal const string UserNotFoundByUserName = "User not found with username: {0}";
        internal const string UserNotFoundByEmailId = "User not found with email: {0}";
        internal const string UserFound = "User: {0} found. validating credentials";
        internal const string GeneratingJWT = "Valid user: {0}. Generating JWT.";
        internal const string IncorrectPassword = "Password incorrect for username/email: {0}";

        internal const string NotAnExistingUser = "Not an existing user. Creating new user: {0}";
        internal const string UserExists = "User already exists with username: {0} or emailid: {1}";

        internal const string GetAllUsers = "Retrieving all the users";

        // Tweet Service Logs
        internal const string AddingTweet = "Adding tweet from user: {0}";
        internal const string TweetAddedByUser = "Tweet Added by user: {0}";
        internal const string Unauthorized = "Not authorized to perform an operation as user: {0} for user: {1}";
        internal const string RetrievingUserNameFromToken = "Retrieving username from JW token";
        internal const string RetrievingAllTweets = "Retrieving all the tweets";
        internal const string AllTweetsRetrieved = "Retrieved all the tweets";
        internal const string MappingRepliesForAllTweets = "Mapping replies of respective tweet for all the tweets";
        internal const string RepliesMappedForAllTweets = "Replies mapped for all the tweets";
        internal const string RetrievingAllTweetsOfUser = "Retrieving all the tweets of user: {0}";
        internal const string AllTweetsRetrievedOfUser = "Retrieved all the tweets of user: {0}";
        internal const string MappingRepliesForAllTweetsOfUser = "Mapping replies of respective tweet for all the tweets of user: {0}";
        internal const string RepliesMappedForAllTweetsOfUser = "Replies mapped for all the tweets of user: {0}";
        internal const string UpdatingTweetById = "Updating tweet by tweetid: {0}";
        internal const string UpdatedTweetById = "Tweet updated by tweetid: {0}";
        internal const string DeletingTweetById = "Deleting tweet by tweetid: {0}";
        internal const string DeletedTweetById = "Tweet deleted by tweetid: {0}";
        internal const string UnauthorizedTweetUpdate = "Tweet with tweetid: {0} cannot be updated as it is posted by user: {1}";
        internal const string UnauthorizedTweetDelete = "Tweet with tweetid: {0} cannot be deleted as it is posted by user: {1}";
        internal const string GettingRepliesByTweetId = "Fetching replies of tweet by tweetid: {0}";
        internal const string FetchedRepliesByTweetId = "Fetched replies of tweet by tweetid: {0}";
        internal const string MapppingRepliesToTweet = "Mapping replies to tweet with tweetid: {0}";
        internal const string MappedRepliesToTweet = "Mapped replies to tweet with tweetid: {0}";

        // Reply Service Logs
        internal const string RetrievingTweetById = "Retrieving tweet by id: {0}";
        internal const string TweetRetrievedById = "Tweet retrieved by id: {0}";
        internal const string TweetNotFound = "Tweet not found with id: {0}";
        internal const string AddingReplyToTweet = "Adding reply to tweet with id: {0} by user: {1}";
        internal const string ReplyAddedToTweet = "Reply added to tweet with id: {0} by user: {1}";
    }
}
