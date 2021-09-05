/** @format */

/* Import Const */
const Discord = require("discord.js");
const client = new Discord.Client();
const config = require("./config");
const fs = require("fs");

/* Config Music */
client.queue = new Map();
client.skipvote = new Map();

/* Commands Registration */
for (let file of fs.readdirSync("./events/")) {
	if (file.endsWith(".js")) {
		let fileName = file.substring(0, file.length - 3);
		let fileContents = require(`./events/${file}`);
		client.on(fileName, fileContents.bind(null, client));
		delete require.cache[require.resolve(`./events/${file}`)];
	}
}

/* Log */
client
	.on("disconnect", () => client.console.warn("Bot is disconnected..."))
	.on("reconnecting", () => client.console.log("Bot reconnecting..."))
	.on("error", (err) => client.console.error(err))
	.on("warn", (info) => client.console.warn(info));

/* Token */
client.login(config.token);
