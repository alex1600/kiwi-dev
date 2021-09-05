/** @format */

const { Command } = require("../../commands")
const Discord = require("discord.js")
const moment = require("moment")

module.exports = class UptimeCommand extends Command {
  constructor() {
    super({
      name: "uptime",
      aliases: ["up"],
      category: "general",
      priority: 9,
      permLvl: 0,
    })
  }
  async execute(message) {
    const d = moment.duration(message.client.uptime)
    const days = d.days() == 1 ? `${d.days()} day` : `${d.days()} days`
    const hours = d.hours() == 1 ? `${d.hours()} hour` : `${d.hours()} hours`
    const minutes = d.minutes() == 1 ? `${d.minutes()} minute` : `${d.minutes()} minutes`
    const seconds = d.seconds() == 1 ? `${d.seconds()} second` : `${d.seconds()} seconds`
    const date = moment().subtract(d, "ms").format("dddd, MMMM Do YYYY")
    const embed = new Discord.MessageEmbed()
      .setTitle("Elyon's Uptime")
      .setThumbnail("https://scontent.fcdg2-1.fna.fbcdn.net/v/t1.6435-9/91859714_100619981609196_5740630017700790272_n.png?_nc_cat=102&ccb=1-3&_nc_sid=09cbfe&_nc_ohc=hFUDbcmSOmkAX-UjTO4&_nc_ht=scontent.fcdg2-1.fna&oh=f531f5936ea0d959a99e661f2b5b6ac9&oe=60946285")
      .setDescription(`\`\`\`prolog\n${days}, ${hours}, ${minutes}, and ${seconds}\`\`\``)
      .addField("Date Launched", date)
      .setFooter(message.member.displayName, message.author.displayAvatarURL({ dynamic: true }))
      .setTimestamp()
      .setColor(message.guild.me.displayHexColor)
    message.channel.send(embed)
  }
}
