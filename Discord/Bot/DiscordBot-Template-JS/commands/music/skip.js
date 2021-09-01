/** @format */

const { Command } = require("../../commands")

module.exports = class skipCommand extends Command {
  constructor() {
    super({
      name: "skip",
      aliases: ["saltar"],
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

    if (msg.member.voice.channel.members.size === 2) {
      msg.channel.send("The song being played was skipped.")
      await serverQueue.connection.dispatcher.end()
      return
    }

    const map = client.skipvote
    const mapload = map.get(msg.guild.id)

    if (mapload) {
      if (mapload.users.includes(msg.author.id)) return msg.reply("You have already voted.")
      await mapload.users.push(msg.author.id)

      if (mapload.users.length > 1) {
        let skipNumber = 1 + parseInt(msg.member.voice.channel.members.size / 2)
        msg.channel.send(`${msg.author.username} has voted to skip the current song. **${mapload.users.length}/${skipNumber}**`)
      }

      const number = parseInt(msg.member.voice.channel.members.size / 2)

      if (mapload.users.length < number) return

      msg.channel.send("The current song was omitted by vote.")

      await serverQueue.connection.dispatcher.end()
    } else {
      const listUser = {
        users: [],
      }
      await map.set(msg.guild.id, listUser)
      await listUser.users.push(msg.author.id)

      let skipNumber = parseInt(msg.member.voice.channel.members.size / 2)

      return msg.channel.send(`**${msg.author.username}** started a new vote to skip the current song. Are needed **${skipNumber}** vote(s) to skip the song.`)
    }
  }
}
