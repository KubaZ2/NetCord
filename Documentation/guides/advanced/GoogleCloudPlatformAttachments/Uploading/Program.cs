﻿using NetCord;
using NetCord.Rest;

using RestClient client = new(new BotToken("Token from Discord Developer Portal"));

ulong channelId = 857933275112800266;
var bucketName = "file.txt";

// Creating Google Cloud Platform Storage Bucket
var buckets = await client.CreateGoogleCloudPlatformStorageBucketsAsync(channelId, [new(bucketName)]);
var bucket = buckets.First();

using (HttpContent fileContent = new StringContent("File content"))
using (HttpClient httpClient = new())
{
    // Uploading file content
    var response = await httpClient.PutAsync(bucket.UploadUrl, fileContent);
    response.EnsureSuccessStatusCode();
}

var uploadFileName = bucket.UploadFileName;
Console.WriteLine(uploadFileName);
