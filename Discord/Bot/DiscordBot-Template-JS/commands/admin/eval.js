/** @format */

const { Command } = require("../../commands")
const Discord = require("discord.js")
const config = require("../../config")
module.exports = class EvalCommand extends Command {
  constructor() {
    super({
      name: "eval",
      aliases: [],
      category: "admin",
      type: config.superusers,
      ownerOnly: true,
      priority: 5,
      permLvl: 3,
    })
  }
  async execute(msg, args) {
    const input = args.join(" ")
    if (!input) return msg.channel.send(msg, 0, "Please provide code to eval")
    if (!input.toLowerCase().includes("token")) {
      const embed = new Discord.MessageEmbed()

      try {
        let output = eval(input)
        if (typeof output !== "string") output = require("util").inspect(output, { depth: 0 })

        embed
          .addField("Input", `\`\`\`js\n${input.length > 1024 ? "Too large to display." : input}\`\`\``)
          .addField("Output", `\`\`\`js\n${output.length > 1024 ? "Too large to display." : output}\`\`\``)
          .setColor("#66FF00")
      } catch (err) {
        embed
          .addField("Input", `\`\`\`js\n${input.length > 1024 ? "Too large to display." : input}\`\`\``)
          .addField("Output", `\`\`\`js\n${err.length > 1024 ? "Too large to display." : err}\`\`\``)
          .setColor("#FF0000")
      }

      msg.channel.send(embed)
    } else {
      msg.channel.send("(╯°□°)╯︵ ┻━┻ MY token. **MINE**.")
    }
  }
}
