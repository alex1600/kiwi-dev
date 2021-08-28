io.stdout:setvbuf('no')

-- Cette ligne permet de déboguer pas à pas dans ZeroBraneStudio
if arg[#arg] == "-debug" then require("mobdebug").start() end

function createQueue()
  local new = {}
  new.list = {}
  new.dequeue = function(self)
    local stack = self.list[1]
    table.remove(self.list, 1)
    return stack
  end
  new.enqueue = function(self, node)
    table.insert(self.list, node)
  end
  new.count = function(self)
    return #self.list
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

local grid = {}
local start = {}
local goal = {}

grid.init = function(self, cs)
  self.cs = cs or 8
  self.w = math.floor(love.graphics.getWidth()/self.cs) - 1
  self.h = math.floor(love.graphics.getHeight()/self.cs) - 1
  for x = 1, self.w do
    self[x] = {}
    for y = 1, self.h do
      self[x][y] = {
        type = "wall",
        x = x,
        y = y
      }
    end
  end
  --self:placeRooms(15,3,6,3,6)
  self.explored = 0
  
  start = self[2][2]
  goal = self[98][72]
  self:maze(start)
  path = self:getPath(start, goal)
end

grid.roomOverlap = function(self, new, rooms)
  for i = 1, #rooms do
    local room = rooms[i]
    if new.x > room.x and new.x < room.x + room.w and new.y > room.y and new.y < room.y + room.h then
      return true
    end
  end
  return false
end

grid.placeRooms = function(self, nRoom, minW, maxW, minH, maxH)
  local rooms = {}
  for i = 1, nRoom do
    local new = {}
    local overlap = true
    while overlap == true do
      new.x = math.random(2*maxW, self.w-2*maxW)
      new.y = math.random(2*maxH, self.h-2*maxH)
      new.w = math.random(minW, maxW)
      new.h = math.random(minH, maxH)
      overlap = self:roomOverlap(new, rooms)
    end
    table.insert(rooms, new)
  end
  
  for i = 1, #rooms do
    for x = rooms[i].x, rooms[i].x + rooms[i].w do
      for y = rooms[i].y, rooms[i].y + rooms[i].h do
        self[x][y].type = "floor"
      end
    end
  end
end

grid.digWall = function(self, node)
  node.type = "floor"
end

grid.adjacents = {
  left = { x = -2, y = 0 },
  right = { x = 2, y = 0 },
  up = { x = 0, y = -2 },
  down = { x = 0, y = 2 }
}

grid.getAdjacents = function(self, node)
  local freeCells = {}
  local x, y = node.x, node.y
  for k, v in pairs(self.adjacents) do
    
    if k == "left" and x > 2 then
      local adjacentCell = self[x + v.x][y + v.y]
      if adjacentCell.type == "wall" then
        table.insert(freeCells, adjacentCell)
      end
    end
    if k == "right" and x < self.w - 2 then
      local adjacentCell = self[x + v.x][y + v.y]
      if adjacentCell.type == "wall" then
        table.insert(freeCells, adjacentCell)
      end
    end
    if k == "up" and y > 2 then
      local adjacentCell = self[x + v.x][y + v.y]
      if adjacentCell.type == "wall" then
        table.insert(freeCells, adjacentCell)
      end
    end
    if k == "down" and y < self.h - 2 then
      local adjacentCell = self[x + v.x][y + v.y]
      if adjacentCell.type == "wall" then
        table.insert(freeCells, adjacentCell)
      end
    end
  end
  return freeCells
end

grid.maze = function(self, currentNode)
  self:digWall(currentNode)
  self.explored = self.explored + 1
  while self.explored < (self.w * self.h) / 2 do
    
    local freeAdjacents = self:getAdjacents(currentNode)
    if #freeAdjacents ~= 0 then
      local r = math.random(1, #freeAdjacents)
      local offset = { x = freeAdjacents[r].x - currentNode.x, y = freeAdjacents[r].y - currentNode.y }
      local toDig = self[currentNode.x + offset.x/2][currentNode.y + offset.y/2]
      local nextSearch = self[currentNode.x + offset.x][currentNode.y + offset.y]
        self:digWall(toDig)
        self:maze(nextSearch)
    else
      break
    end
  end
end

grid.getFloorAdjacents = function(self, node)
  local freeCells = {}
  local adj = {
    left = { x = -1, y = 0 },
    right = { x = 1, y = 0 },
    up = { x = 0, y = -1 },
    down = { x = 0, y = 1 }
  }
  for k, v in pairs(adj) do
    local adjacentCell = self[node.x + v.x][node.y + v.y]
    if adjacentCell.type == "floor" then
      table.insert(freeCells, adjacentCell)
    end
  end
  return freeCells
end

grid.getPath = function(self, start, goal)
  local queue = createQueue()
  local exploredNodes = {}
  queue:enqueue(start)
  
  while queue:count() ~= 0 do
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
    for _, node in pairs(self:getFloorAdjacents(currentNode)) do
      if not table.contains(exploredNodes, node) then
        table.insert(exploredNodes, node)
        node.parent = currentNode
        queue:enqueue(node)
      end
    end
  end
  return start
end

function love.load()
  grid:init(8)
end

function love.update(dt)
  
end

function love.keypressed(key)
  if key == "space" then
    grid:init()
  end
end

function love.mousepressed(x, y, button)
  if x > 0 and x < grid.w * grid.cs then
    if y > 0 and y < grid.h * grid.cs then
      local tx = math.ceil(x/grid.cs)
      local ty = math.ceil(y/grid.cs)
      local cell = grid[tx][ty]
      if cell.type == "floor" then
        if button == 1 then
          start = cell
        elseif button == 2 then
          goal = cell
        end
      end
    end
  end
  path = grid:getPath(start, goal)
end

function love.draw()
  love.graphics.setColor(255,255,255,80)
  for x = 1, #grid do
    for y = 1, #grid[x] do
      if grid[x][y].type == "floor" then
        love.graphics.rectangle("fill", (x-1) * grid.cs, (y-1) * grid.cs, grid.cs, grid.cs)
      end
    end
  end
  love.graphics.setColor(0,255,0, 50)

  for i = 1, #path do
    local node = path[i]
    love.graphics.rectangle("fill", (node.x - 1) * grid.cs, (node.y - 1) * grid.cs, grid.cs, grid.cs)
  end
  love.graphics.setColor(255,0,0)
  love.graphics.rectangle("fill", (start.x - 1) * grid.cs, (start.y - 1) * grid.cs, grid.cs, grid.cs)
  love.graphics.setColor(0,0,255)
  love.graphics.rectangle("fill", (goal.x - 1) * grid.cs, (goal.y -1) * grid.cs, grid.cs, grid.cs)
end