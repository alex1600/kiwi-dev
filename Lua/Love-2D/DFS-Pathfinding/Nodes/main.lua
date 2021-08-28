io.stdout:setvbuf('no')
if arg[#arg] == "-debug" then require("mobdebug").start() end

function math.dist(x1,y1, x2,y2) return ((x2-x1)^2+(y2-y1)^2)^0.5 end

math.randomseed(os.time())

love.window.setFullscreen(true)

sW, sH = love.graphics.getDimensions()

local infinity = 3e354

local stars = {}
local explored = 0
local maxDist = 150

local path = {}
local step = 1
local start
local goal



local selectedStar = stars[1]

function createQueue()
  local new = {}
  new.list = {}
  new.dequeue = function(self)
    local node = self.list[1]
    table.remove(self.list, 1)
    return node
  end
  new.enqueue = function(self, node)
    table.insert(self.list, node)
  end
  return new
end

function table.contains(table, element)
  for _, value in pairs(table) do
    if value == element then
      return true
    end
  end
  return false
end

function dfs(start, goal)
  local queue = createQueue()
  local exploredNodes = {}
  queue:enqueue(start)
  
  while #queue.list ~= 0 do
    local currentNode = queue:dequeue()
    if currentNode == goal then
      local path = {}
      while currentNode.parent ~= start do
        table.insert(path, currentNode)
        currentNode = currentNode.parent
      end
      
      table.insert(path, currentNode)
      table.insert(path, start)
      return path
    end
    
    local nodes = currentNode.childs
    for k, node in pairs(nodes) do
      if not table.contains(exploredNodes, node) then
        table.insert(exploredNodes, node)
        node.parent = currentNode
        queue:enqueue(node)
      end
    end
  end
  return start
end

function getStarsAround(node)
  local freeStars = {}
  
  for i = 1, #stars.list do
    local star = stars.list[i]
    if math.dist(star.x, star.y, node.x, node.y) < maxDist and math.dist(star.x, star.y, node.x, node.y) ~= 0 then
      table.insert(freeStars, star)
    end
  end

  return freeStars
end

function getFreeStarsAround(pFreeStars)
  local freeStars = {}
  
  for i = 1, #stars.list do
    local star = stars.list[i]
    if star.explored == false then
      table.insert(freeStars, star)
    end
  end

  return freeStars
end

function explore(node)
  node.explored = true
  explored = explored + 1
  while explored <= #stars.list do
    local starsAround = getStarsAround(node)
    for i = 1, #starsAround do
      table.insert(node.childs, starsAround[i])
    end
    local freeStars = getFreeStarsAround(starsAround)
    if #freeStars == 0 then
      break
    else
      local randomStar = freeStars[math.random(1, #freeStars)]
      explore(randomStar)
    end
  end
end

function init(nbStar)
  nbStar = nbStar or 100
  step = 99
  explored = 0
  stars = {}
  stars.list = {}
  for i = 1, nbStar do
    stars.list[i] = {
      x = math.random(0, sW),
      y = math.random(0, sH),
      childs = {},
      explored = false,
      selected = false,
    }
  end
  stars.selected = stars.list[1]
  stars.list[1].selected = true 
  explore(stars.list[1])
  
  start = stars.list[1]
  goal = stars.list[50]
  path = dfs(start, goal)
end

function love.load()
  init(150)
end

function love.update(dt)

end

function love.mousepressed(x, y, button)
  for i = 1, #stars.list do
    local star = stars.list[i]
    if x - 5 < star.x and x + 5 > star.x and y - 5 < star.y and y + 5 > star.y then
      if button == 1 then
        start = star
      elseif button == 2 then
        goal = star
      end
    end
  end
  path = dfs(start, goal)
end

function love.keypressed(key)
  if key == "space" then
    init()
  end
  if key == "escape" then
    love.event.quit()
  end
end

function drawPath(step)
  love.graphics.setColor(0,255,0)

  for i = 1, #path do
    if i > 1 then
      local node = path[i-1]
      local nextNode = path[i]
      love.graphics.line(node.x, node.y, nextNode.x, nextNode.y)
    end
  end
end

function drawChilds(star)
  love.graphics.setColor(255,255,255,20)
  for _, child in pairs(star.childs) do
    love.graphics.line(star.x, star.y, child.x, child.y)
  end
  love.graphics.setColor(255,255,255)
end

function drawStars(pDrawChilds)
  love.graphics.setColor(255,255,255)
  for i = 1, #stars.list do
    local star = stars.list[i]
    if star.x == start.x then
      love.graphics.setColor(0,0,255)
    elseif star.x == goal.x then
      love.graphics.setColor(0,127,127)
    end
    if pDrawChilds then
      drawChilds(star)
    end
    love.graphics.circle("fill", star.x, star.y, 5)
  end
end

function drawSelectedStar(pDrawChilds)
  love.graphics.setColor(255,0,0)
  if pDrawChilds then
    for _, child in pairs(stars.selected.childs) do
      love.graphics.line(stars.selected.x, stars.selected.y, child.x, child.y)
    end
  end
  love.graphics.circle("fill", stars.selected.x, stars.selected.y, 5)
end


function love.draw()
  drawStars(true)
  --drawSelectedStar(true)
  drawPath()
  love.graphics.setColor(255,0,0)
  love.graphics.circle("line", start.x, start.y, 20)
  love.graphics.setColor(0,0,255)
  love.graphics.circle("line", goal.x, goal.y, 20)
end