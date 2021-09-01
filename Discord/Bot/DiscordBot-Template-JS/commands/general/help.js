/** @format */

const { Command } = require("../../commands");

module.exports = class help extends Command {
	constructor() {
		super({
			name: "help",
			aliases: ["h"],
			category: "general",
			priority: 9,
			permLvl: 0,
		});
	}
	async execute(msg) {
		msg.channel.send("EUH ?");
	}
};
