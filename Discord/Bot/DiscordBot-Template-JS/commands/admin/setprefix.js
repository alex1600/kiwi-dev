/** @format */

const { Command } = require("../../commands")

const db = require("../../database/database")

module.exports = class setPrefixCommand extends Command {
  constructor() {
    super({
      name: "setprefix",
      aliases: ["spe"],
      category: "admin",
      priority: 5,
      permLvl: 3,
    })
  }
  async execute(msg, args) {
    if (!args[0]) return msg.channel.send(`You must add a symbol as a custom prefix.`)

    let exists = await db.admin.existsPrefix(msg.guild.id)
    if (exists) {
      await db.admin.updatePrefix(msg.guild.id, args[0])
    } else {
      await db.admin.addPrefix(msg.guild.id, args[0])
    }
    msg.channel.send(`New prefix **${args[0]}**, for **${msg.guild.name}**`)
  }
}
