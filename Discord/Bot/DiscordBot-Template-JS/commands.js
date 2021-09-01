/** @format */

const fs = require("fs");
const config = require("./config.js");
//const

class Command {
	constructor(commandInfo) {
		this.name = commandInfo.name;
		this.args = commandInfo.args;
		this.category = commandInfo.category;
		this.aliases = commandInfo.aliases;
		this.permLvl = commandInfo.permLvl;
		this.priority = commandInfo.priority;
	}
	checkArgs(msg, msgArgs) {
		var valid = true;
		if (this.args != undefined) {
			if (msgArgs.length == 0 && this.args.find((x) => !x.optional) != undefined) {
				return false;
			}
			let argsPos = 0;
			for (let cmdArg of this.args) {
				if (cmdArg[argsPos] != undefined) {
					if (!cmdArg.optional) {
						break;
					}
				} else {
					if (!cmdArg.checkArg(msg, msgArgs[argsPos])) {
						if (!cmdArg.optional || cmdArg.failOnInvalid) {
							valid = false;
							break;
						}
					} else {
						if (cmdArg.breakOnValid) {
							break;
						}
						argsPos++;
					}
				}
			}
		}
		return valid;
	}
}

class Argument {
	constructor(argInfo) {
		this.optional = argInfo.optional;
		this.type = argInfo.type;
		this.interactiveMsg = argInfo.interactiveMsg;
		this.possibleValues = argInfo.possibleValues;
		this.missingError = argInfo.missingError;
		this.invalidError = argInfo.invalidError;
	}
	checkArg(msg, msgArg) {
		var valid = true;

		switch (this.type) {
			case "mention":
				let mention = msgArg.match(/<@!?(.*?[0-9])>/);
				if (mention == null || !msg.guild.members.cache.has(mention[1])) {
					valid = false;
				}
				break;
			case "int":
				if (!Number(msgArg)) {
					valid = false;
				}
				break;
			case "channel":
				let channel = msgArg.match(/<#(.*?)>/);
				if (channel == null || !msg.guild.channels.cache.has(channel[1])) {
					valid = false;
				}
				break;
		}
		return valid;
	}
}

class Category {
	constructor(categoryInfo) {
		this.name = categoryInfo.name;
		this.priority = categoryInfo.priority;
		this.commands = new Map();
	}
	addCommand(command) {
		this.commands.set(command.name, command);
	}
}

module.exports = {
	Command: Command,
	Argument: Argument,
	Category: Category,
	namesAliases: [],
	categories: new Map(),
	commands: new Map(),
	registerCategories: function (categories) {
		for (category of categories) {
			var category = new Category(category);
			this.categories.set(category.name, category);
		}
	},
	loadFile: function (path) {
		return require(path);
	},
	registerCommands: function () {
		console.log(`----------\nCMDs | Alias | Category\n----------`);
		var cmds = fs.readdirSync(`./commands/`);
		for (var module of cmds) {
			var files = fs.readdirSync(`./commands/${module}`);
			for (var file of files) {
				if (fs.statSync(`./commands/${module}/${file}`).isFile()) {
					var keys = this.loadFile(`./commands/${module}/${file}`);
					if (typeof keys != "object") {
						keys = {
							keys,
						};
					}
					for (var key in keys) {
						if (keys[key].prototype instanceof Command) {
							var command = new keys[key]();
							console.log(`${command.name} || ${command.aliases} FOR ${command.category}`);
							if (!this.categories.has(module)) {
								this.registerCategories([module]);
							}
							this.commands.set(command.name, command);
							this.namesAliases.push(command.name, ...command.aliases);
							this.categories.get(module).addCommand(command);
						}
					}
				}
			}
		}
	},
	checkPerms: function (msg, permLvl) {
		for (var i = 0; i < config.superusers.length; i++) {
			if (msg.author.id === config.superusers[i]) {
				return true;
			}
		}

		let perms = msg.member.permissions;
		if (perms.has("ADMINISTRATOR")) {
			return true;
		}

		let userPermsLvl = 1;
		if (userPermsLvl >= permLvl) {
			return true;
		}
		return false;
	},
	getCmd: function (arg) {
		var command = this.commands.get(arg);

		if (!command) {
			this.commands.forEach(function (aCmd) {
				if (aCmd.aliases.includes(arg)) {
					command = aCmd;
					return;
				}
			});
		}
		return command;
	},
	checkValidCmd: async function (msg, args, prefix) {
		var command = this.getCmd(args[0]);

		if (msg.content.startsWith(prefix) && command != null) {
			let result = this.checkPerms(msg, command.permLvl);
			if (result) {
				return true;
			}
		}

		return false;
	},
	executeCmd: async function (msg, args, discord, client) {
		let cmd = this.getCmd(args[0]);
		if (cmd.checkArgs(msg, args.slice(1))) {
			await cmd.execute(msg, args.slice(1), discord, client);
		}
	},
};
