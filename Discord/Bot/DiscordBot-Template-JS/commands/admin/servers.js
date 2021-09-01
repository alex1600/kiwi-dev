/** @format */

const { Command } = require("../../commands")
const Discord = require("discord.js")
const config = require("../../config")

module.exports = class ServersCommand extends Command {
  constructor() {
    super({
      name: "servers",
      aliases: ["servs"],
      category: "admin",
      type: config.superusers,
      ownerOnly: true,
      priority: 5,
      permLvl: 3,
    })
  }
  async execute(msg, args) {
    const servers = msg.client.guilds.cache.array().map((guild) => {
      return `\`${guild.id}\` - **${guild.name}** - \`${guild.members.cache.size}\` members`
    })

    const embed = new Discord.MessageEmbed()
      .setTitle("Server List")
      .setFooter(msg.member.displayName, msg.author.displayAvatarURL({ dynamic: true }))
      .setTimestamp()
      .setColor(msg.guild.me.displayHexColor)

    if (servers.length <= 10) {
      const range = servers.length == 1 ? "[1]" : `[1 - ${servers.length}]`
      msg.channel.send(embed.setTitle(`Server List ${range}`).setDescription(servers.join("\n")))
    }
  }
}
