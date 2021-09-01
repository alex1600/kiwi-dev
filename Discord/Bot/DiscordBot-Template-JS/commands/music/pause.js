/** @format */

const { Command } = require("../../commands")

module.exports = class pauseCommand extends Command {
  constructor() {
    super({
      name: "pause",
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

    if (serverQueue && serverQueue.playing) {
      serverQueue.playing = false
      console.log("En pause")
      serverQueue.connection.dispatcher.pause(true)

      return msg.channel.send("Paused song")
    }
  }
}
