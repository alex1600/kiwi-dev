/** @format */

const { Command } = require("../../commands");
const Discord = require("discord.js");

module.exports = class PingCommand extends Command {
	constructor() {
		super({
			name: "ping",
			aliases: [],
			category: "general",
			priority: 9,
			permLvl: 0,
		});
	}
	async execute(message) {
		const embed = new Discord.MessageEmbed().setDescription("`Pinging...`").setColor(message.guild.me.displayHexColor);
		const msg = await message.channel.send(embed);
		const timestamp = message.editedTimestamp ? message.editedTimestamp : message.createdTimestamp; // Check if edited
		const latency = `\`\`\`ini\n[ ${Math.floor(msg.createdTimestamp - timestamp)}ms ]\`\`\``;
		const apiLatency = `\`\`\`ini\n[ ${Math.round(message.client.ws.ping)}ms ]\`\`\``;
		embed
			.setTitle(`Pong! `)
			.setDescription("")
			.addField("Latency", latency, true)
			.addField("API Latency", apiLatency, true)
			.setFooter(message.member.displayName, message.author.displayAvatarURL({ dynamic: true }))
			.setTimestamp();
		msg.edit(embed);
	}
};
