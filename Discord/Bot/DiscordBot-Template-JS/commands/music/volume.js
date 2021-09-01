/** @format */

const { Command } = require("../../commands")

module.exports = class volumeCommand extends Command {
  constructor() {
    super({
      name: "volume",
      aliases: ["vol"],
      category: "music",
      priority: 7,
      permLvl: 0,
    })
  }
  async execute(msg, args, discord, client) {
    const queue = client.queue
    const serverQueue = queue.get(msg.guild.id)
    let volume = args[0] / 100
    volume = Number(volume)

    if (!msg.member.voice.channel) return msg.channel.send("You are not connecting on a voice channel.")
    if (!serverQueue) return msg.channel.send("There are no songs playing.")

    if (!volume) return msg.channel.send("You must add a value for the volume.")
    if (volume > 100) return msg.channel.send("The quantity for the volume value must be less than 100")

    serverQueue.volume = volume
    serverQueue.connection.dispatcher.setVolume(volume)

    return msg.channel.send(`Now the volume for the songs is: **${volume * 100}**`)
  }
}
