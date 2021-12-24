function descriptor()
	return { title = "Timestamp to txt";
		 version = "0.2";
		 author = "Spiros Zervos";		
		 url = 'https://www.linkedin.com/in/spiros-zervos-0478b5154/';
		 shortdesc = "Timestamp to txt";
		 description = "Outputs the current timestamp to a txt whenever a button is pressed.";
		 capabilities = { }
		}
end

file = nil
count = 0

function activate()
	local item = vlc.input.item()
	local uri = item:uri()
	uri = string.gsub(uri, '^file:///', '')
	uri = string.gsub(uri, '/', '\\')
	uri = string.gsub(uri, '%%20', ' ')
	uri = string.gsub(uri, '.mp4', '.txt')
	file = io.open("timestamps.txt", "w")
	d = vlc.dialog( "Timestamp" )
	d:add_button("Timestamp", txtExport)
end

function txtExport()
	local input = vlc.object.input() local m_current = vlc.var.get(input, "time")
	local current = m_current/1000000
	local currentHour = math.floor(current/3600)
	local currentMinute = math.floor((current%3600)/60)
	local currentSecond = math.floor((current%60))
	local outputstring = string.format("%02d:%02d:%02d",currentHour,currentMinute,currentSecond)
	file:write(outputstring)
	count = count + 1
	if (count%2 == 0) then
		file:write("\n")
	else
		file:write("-")
	end
end

function deactivate()
	
end