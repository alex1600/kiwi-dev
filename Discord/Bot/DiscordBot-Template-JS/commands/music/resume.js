/** @format */

const { Command } = require("../../commands")

module.exports = class resumeCommand extends Command {
  constructor() {
    super({
      name: "resume",
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

    if (serverQueue && !serverQueue.playing) {
      serverQueue.playing = true
      console.log("Resuming")
      serverQueue.connection.dispatcher.resume()

      return msg.channel.send("Song resumed.")
    }

    msg.channel.send("There are no songs stopped.")
  }
}
