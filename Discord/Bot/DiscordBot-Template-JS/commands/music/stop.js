/** @format */

const { Command } = require("../../commands")

module.exports = class stopCommand extends Command {
  constructor() {
    super({
      name: "stop",
      aliases: [],
      category: "music",
      priority: 7,
      permLvl: 0,
    })
  }
  async execute(msg, args, discord, client) {
    const queue = client.queue
    const serverQueue = queue.get(msg.guild.id)

    if (!msg.member.voice.channel) return msg.channel.send("You are not connecting on a voice channel.")
    if (!serverQueue) return msg.channel.send("There are no songs playing.")

    serverQueue.songs = []

    await serverQueue.connection.dispatcher.end()
    return msg.channel.send("Songs stopped.")
  }
}
